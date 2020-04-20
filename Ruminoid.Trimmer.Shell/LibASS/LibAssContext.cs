using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Ruminoid.Trimmer.Shell.LibASS;
using Unosquare.FFME.Common;
using static Ruminoid.Trimmer.Shell.LibASS.LibASSInterop;

namespace Ruminoid.Trimmer.Shell.LibAss
{
    public class LibASSContext : IDisposable
    {
        private const string DefaultAssString = @"[Script Info]
ScriptType: v4.00+
PlayResX: 384
PlayResY: 288

[V4+ Styles]
Format: Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, Italic, Underline, StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, Encoding
Style: Default,Arial,48,&H00FFFFFF,&H000000FF,&H00000000,&H00000000,0,0,0,0,100,100,0,0,1,2,2,2,10,10,10,1

[Events]
Format: Layer, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text
Dialogue: 0,0:00:00.10,1:00:00.20,Default,,0,0,0,,LibASS Running";

        private const string DefaultCodepage = "UTF-8";
        private static IntPtr _library;
        private static IntPtr _renderer;
        private static IntPtr _assStringPtr;
        private static IntPtr _assCodepagePtr;

        private static ASS_MessageCallback _msg_cb = (level, fmt, args, data) =>
        {
            var sb = new StringBuilder(_vscprintf(fmt, args) + 1);
            vsprintf(sb, fmt, args);
            Console.WriteLine($@"[libass] {sb}");
        };

        static LibASSContext()
        {
            _library = ass_library_init();
            _renderer = ass_renderer_init(_library);
            _assStringPtr = Marshal.StringToHGlobalAnsi(DefaultAssString);
            _assCodepagePtr = Marshal.StringToHGlobalAnsi(DefaultCodepage);
            ass_set_message_cb(_library, _msg_cb, IntPtr.Zero);
            ass_set_fonts(_renderer, IntPtr.Zero, "sans-serif", 1, IntPtr.Zero, 1);
        }

        private IntPtr _track;
        private IntPtr _event;
        private ASS_Event _eventMarshaled;
        private IntPtr _gString = IntPtr.Zero;
        private IntPtr _origString = IntPtr.Zero;

        public LibASSContext()
        {
            _track = ass_read_memory(_library, _assStringPtr, DefaultAssString.Length, _assCodepagePtr);

            var track = Marshal.PtrToStructure<ASS_Track>(_track);
            _event = track.events;
            _eventMarshaled = Marshal.PtrToStructure<ASS_Event>(_event);
            _origString = _eventMarshaled.Text;
        }

        public void UpdateRenderSize(int width, int height)
        {
            ass_set_frame_size(_renderer, width, height);
        }

        public void Update(int start, int duration, string text)
        {
            if (_gString != IntPtr.Zero)
                Marshal.FreeHGlobal(_gString);
            _gString = IntPtr.Zero;
            _eventMarshaled.Start = start;
            _eventMarshaled.Duration = duration;

            var textUTF8 = Encoding.UTF8.GetBytes(text);
            var gString = Marshal.AllocHGlobal(textUTF8.Length + 1);
            Marshal.Copy(textUTF8, 0, gString, textUTF8.Length);
            Marshal.WriteByte(gString, textUTF8.Length, 0); // add \0
            _eventMarshaled.Text = gString;
            Marshal.StructureToPtr(_eventMarshaled, _event, false);

            _gString = gString;
        }

        public void RenderAndBlend(int current, BitmapDataBuffer data)
        {
            UpdateRenderSize(data.PixelWidth, data.PixelHeight);
            var updated = 0;
            var imageRaw = ass_render_frame(_renderer, _track, current, ref updated);
            if (imageRaw == IntPtr.Zero)
                return;
            try
            {
                unsafe
                {
                    var sourceRaw = (uint*)data.Scan0.ToPointer();
                    int srcStride = data.Stride / sizeof(uint);
                    while (imageRaw != IntPtr.Zero)
                    {
                        var image = Marshal.PtrToStructure<ASS_Image>(imageRaw);
                        var imgRaw = (byte*)image.bitmap.ToPointer();
                        int h = image.h, w = image.w;
                        if (h != 0 && w != 0)
                        {
                            int dstStride = image.stride;
                            uint color = image.color;
                            int srcCurrentPixel, dstCurrentPixel;
                            uint image1Pixel;
                            byte image2Pixel;
                            uint srcRed, dstRed, finRed;
                            uint srcGreen, dstGreen, finGreen;
                            uint srcBlue, dstBlue, finBlue;
                            uint dstAlpha, srcAlpha, finAlpha;

                            // RGBA
                            dstAlpha = 255 - (color & 0xFF);
                            dstRed = ((color >> 24) & 0xFF) * dstAlpha / 255;
                            dstGreen = ((color >> 16) & 0xFF) * dstAlpha / 255;
                            dstBlue = ((color >> 8) & 0xFF) * dstAlpha / 255;

                            for (var x = 0; x < w; x++)
                            {
                                for (var y = 0; y < h; y++)
                                {
                                    srcCurrentPixel = (y + image.dst_y) * srcStride + x + image.dst_x;
                                    dstCurrentPixel = y * dstStride + x;
                                    image1Pixel = sourceRaw[srcCurrentPixel];
                                    image2Pixel = imgRaw[dstCurrentPixel];

                                    // ARGB
                                    srcAlpha = ((image1Pixel >> 24) & 0xFF);
                                    srcRed = ((image1Pixel >> 16) & 0xFF) * srcAlpha / 255;
                                    srcGreen = ((image1Pixel >> 8) & 0xFF) * srcAlpha / 255;
                                    srcBlue = (image1Pixel & 0xFF) * srcAlpha / 255;

                                    uint dstAlpha2 = image2Pixel;
                                    uint srcAlpha2 = 255 - dstAlpha2;

                                    finRed = dstRed * dstAlpha2 / 255 + srcRed * srcAlpha2 / 255;
                                    finGreen = dstGreen * dstAlpha2 / 255 + srcGreen * srcAlpha2 / 255;
                                    finBlue = dstBlue * dstAlpha2 / 255 + srcBlue * srcAlpha2 / 255;
                                    finAlpha = dstAlpha2 + srcAlpha * srcAlpha2 / 256;

                                    // ARGB
                                    sourceRaw[srcCurrentPixel] =
                                        finBlue | finGreen << 8 | finRed << 16 | finAlpha << 24;
                                }
                            }
                        }

                        imageRaw = image.next;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"[libass] Exception rendering subtitle: " + e);
            }
        }

        public void Dispose()
        {
            _eventMarshaled.Text = _origString;
            Marshal.StructureToPtr(_eventMarshaled, _event, false);
            ass_free_track(_track);

            if (_gString != IntPtr.Zero)
                Marshal.FreeHGlobal(_gString);
        }
    }
}
