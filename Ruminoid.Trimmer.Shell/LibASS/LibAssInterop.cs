using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace Ruminoid.Trimmer.Shell.LibASS
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct ASS_Style
    {
        /// char*
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
        public string Name;
        /// char*
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
        public string FontName;
        /// double
        public double FontSize;
        /// unsigned int
        public uint PrimaryColour;
        /// unsigned int
        public uint SecondaryColour;
        /// unsigned int
        public uint OutlineColour;
        /// unsigned int
        public uint BackColour;
        /// int
        public int Bold;
        /// int
        public int Italic;
        /// int
        public int Underline;
        /// int
        public int StrikeOut;
        /// double
        public double ScaleX;
        /// double
        public double ScaleY;
        /// double
        public double Spacing;
        /// double
        public double Angle;
        /// int
        public int BorderStyle;
        /// double
        public double Outline;
        /// double
        public double Shadow;
        /// int
        public int Alignment;
        /// int
        public int MarginL;
        /// int
        public int MarginR;
        /// int
        public int MarginV;
        /// int
        public int Encoding;
        /// int
        public int treat_fontname_as_pattern;
        /// double
        public double Blur;
        /// int
        public int Justify;
    }
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct ASS_Event
    {
        /// int
        public long Start;
        /// int
        public long Duration;
        /// int
        public int ReadOrder;
        /// int
        public int Layer;
        /// int
        public int Style;
        /// char*
        public IntPtr Name;
        /// int
        public int MarginL;
        /// int
        public int MarginR;
        /// int
        public int MarginV;
        /// char*
        public IntPtr Effect;
        /// char*
        public IntPtr Text;
        /// ASS_RenderPriv*
        public IntPtr render_priv;
    }
    public enum ASS_YCbCrMatrix
    {
        /// YCBCR_DEFAULT -> 0
        YCBCR_DEFAULT = 0,
        YCBCR_UNKNOWN,
        YCBCR_NONE,
        YCBCR_BT601_TV,
        YCBCR_BT601_PC,
        YCBCR_BT709_TV,
        YCBCR_BT709_PC,
        YCBCR_SMPTE240M_TV,
        YCBCR_SMPTE240M_PC,
        YCBCR_FCC_TV,
        YCBCR_FCC_PC,
    }
    public enum ASS_TrackType
    {
        /// TRACK_TYPE_UNKNOWN -> 0
        TRACK_TYPE_UNKNOWN = 0,
        TRACK_TYPE_ASS,
        TRACK_TYPE_SSA,
    }
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct ASS_Track
    {
        /// int
        public int n_styles;
        /// int
        public int max_styles;
        /// int
        public int n_events;
        /// int
        public int max_events;
        /// ASS_Style*
        public IntPtr styles;
        /// ASS_Event*
        public IntPtr events;
        /// char*
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
        public string style_format;
        /// char*
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
        public string event_format;
        /// Anonymous_b063b2ce_7b04_4b93_9600_5be693d65d90
        public ASS_TrackType track_type;
        /// int
        public int PlayResX;
        /// int
        public int PlayResY;
        /// double
        public double Timer;
        /// int
        public int WrapStyle;
        /// int
        public int ScaledBorderAndShadow;
        /// int
        public int Kerning;
        /// char*
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
        public string Language;
        /// ASS_YCbCrMatrix
        public ASS_YCbCrMatrix YCbCrMatrix;
        /// int
        public int default_style;
        /// char*
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
        public string name;
        /// ASS_Library*
        public IntPtr library;
        /// ASS_ParserPriv*
        public IntPtr parser_priv;
    }
    public enum ASS_ImageType
    {
        IMAGE_TYPE_CHARACTER,
        IMAGE_TYPE_OUTLINE,
        IMAGE_TYPE_SHADOW,
    }
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct ASS_Image
    {
        /// int
        public int w;
        /// int
        public int h;
        /// int
        public int stride;
        /// unsigned char*
        public IntPtr bitmap;
        /// unsigned int
        public uint color;
        /// int
        public int dst_x;
        /// int
        public int dst_y;
        /// ass_image*
        public IntPtr next;
        /// Anonymous_f80580e8_2589_479a_8829_44cae9f6edc0
        public ASS_ImageType type;
    }
    public enum ASS_Hinting
    {
        /// ASS_HINTING_NONE -> 0
        ASS_HINTING_NONE = 0,
        ASS_HINTING_LIGHT,
        ASS_HINTING_NORMAL,
        ASS_HINTING_NATIVE,
    }
    public enum ASS_ShapingLevel
    {
        /// ASS_SHAPING_SIMPLE -> 0
        ASS_SHAPING_SIMPLE = 0,
        ASS_SHAPING_COMPLEX,
    }
    public enum ASS_OverrideBits
    {
        /// ASS_OVERRIDE_DEFAULT -> 0
        ASS_OVERRIDE_DEFAULT = 0,
        /// ASS_OVERRIDE_BIT_STYLE -> 1<<0
        ASS_OVERRIDE_BIT_STYLE = (1) << (0),
        /// ASS_OVERRIDE_BIT_SELECTIVE_FONT_SCALE -> 1<<1
        ASS_OVERRIDE_BIT_SELECTIVE_FONT_SCALE = (1) << (1),
        /// ASS_OVERRIDE_BIT_FONT_SIZE -> 1<<1
        ASS_OVERRIDE_BIT_FONT_SIZE = (1) << (1),
        /// ASS_OVERRIDE_BIT_FONT_SIZE_FIELDS -> 1<<2
        ASS_OVERRIDE_BIT_FONT_SIZE_FIELDS = (1) << (2),
        /// ASS_OVERRIDE_BIT_FONT_NAME -> 1<<3
        ASS_OVERRIDE_BIT_FONT_NAME = (1) << (3),
        /// ASS_OVERRIDE_BIT_COLORS -> 1<<4
        ASS_OVERRIDE_BIT_COLORS = (1) << (4),
        /// ASS_OVERRIDE_BIT_ATTRIBUTES -> 1<<5
        ASS_OVERRIDE_BIT_ATTRIBUTES = (1) << (5),
        /// ASS_OVERRIDE_BIT_BORDER -> 1<<6
        ASS_OVERRIDE_BIT_BORDER = (1) << (6),
        /// ASS_OVERRIDE_BIT_ALIGNMENT -> 1<<7
        ASS_OVERRIDE_BIT_ALIGNMENT = (1) << (7),
        /// ASS_OVERRIDE_BIT_MARGINS -> 1<<8
        ASS_OVERRIDE_BIT_MARGINS = (1) << (8),
        /// ASS_OVERRIDE_FULL_STYLE -> 1<<9
        ASS_OVERRIDE_FULL_STYLE = (1) << (9),
        /// ASS_OVERRIDE_BIT_JUSTIFY -> 1<<10
        ASS_OVERRIDE_BIT_JUSTIFY = (1) << (10),
    }
    public enum ASS_DefaultFontProvider
    {
        /// ASS_FONTPROVIDER_NONE -> 0
        ASS_FONTPROVIDER_NONE = 0,
        /// ASS_FONTPROVIDER_AUTODETECT -> 1
        ASS_FONTPROVIDER_AUTODETECT = 1,
        ASS_FONTPROVIDER_CORETEXT,
        ASS_FONTPROVIDER_FONTCONFIG,
        ASS_FONTPROVIDER_DIRECTWRITE,
    }
    /// Return Type: void
    ///level: int
    ///fmt: char*
    ///args: va_list->char*
    ///data: void*
    public delegate void ASS_MessageCallback(int level, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)] string fmt, IntPtr args, IntPtr data);
    public class LibASSInterop
    {
        [System.Runtime.InteropServices.DllImport("msvcrt.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int vsprintf(
            StringBuilder buffer,
            string format,
            IntPtr args);
        [System.Runtime.InteropServices.DllImport("msvcrt.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int _vscprintf(
            string format,
            IntPtr ptr);

        /// Return Type: int
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_library_version")]
        public static extern int ass_library_version();

        /// Return Type: ASS_Library*
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_library_init")]
        public static extern IntPtr ass_library_init();

        /// Return Type: void
        ///priv: ASS_Library*
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_library_done")]
        public static extern void ass_library_done(IntPtr priv);

        /// Return Type: void
        ///priv: ASS_Library*
        ///fonts_dir: char*
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_set_fonts_dir")]
        public static extern void ass_set_fonts_dir(IntPtr priv, [System.Runtime.InteropServices.In()] [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)] string fonts_dir);

        /// Return Type: void
        ///priv: ASS_Library*
        ///extract: int
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_set_extract_fonts")]
        public static extern void ass_set_extract_fonts(IntPtr priv, int extract);

        /// Return Type: void
        ///priv: ASS_Library*
        ///list: char**
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_set_style_overrides")]
        public static extern void ass_set_style_overrides(IntPtr priv, ref IntPtr list);

        /// Return Type: void
        ///track: ASS_Track*
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_process_force_style")]
        public static extern void ass_process_force_style(ref ASS_Track track);

        /// Return Type: void
        ///priv: ASS_Library*
        ///msg_cb: Anonymous_0c9adf70_369b_4f1a_88c2_a8f7d911137f
        ///data: void*
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_set_message_cb")]
        public static extern void ass_set_message_cb(IntPtr priv, ASS_MessageCallback msg_cb, IntPtr data);

        /// Return Type: ASS_Renderer*
        ///param0: ASS_Library*
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_renderer_init")]
        public static extern IntPtr ass_renderer_init(IntPtr param0);

        /// Return Type: void
        ///priv: ASS_Renderer*
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_renderer_done")]
        public static extern void ass_renderer_done(IntPtr priv);

        /// Return Type: void
        ///priv: ASS_Renderer*
        ///w: int
        ///h: int
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_set_frame_size")]
        public static extern void ass_set_frame_size(IntPtr priv, int w, int h);

        /// Return Type: void
        ///priv: ASS_Renderer*
        ///w: int
        ///h: int
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_set_storage_size")]
        public static extern void ass_set_storage_size(IntPtr priv, int w, int h);

        /// Return Type: void
        ///priv: ASS_Renderer*
        ///level: ASS_ShapingLevel->Anonymous_baa04d3c_3af5_4409_8d1b_fe9fa5c59620
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_set_shaper")]
        public static extern void ass_set_shaper(IntPtr priv, ASS_ShapingLevel level);

        /// Return Type: void
        ///priv: ASS_Renderer*
        ///t: int
        ///b: int
        ///l: int
        ///r: int
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_set_margins")]
        public static extern void ass_set_margins(IntPtr priv, int t, int b, int l, int r);

        /// Return Type: void
        ///priv: ASS_Renderer*
        ///use: int
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_set_use_margins")]
        public static extern void ass_set_use_margins(IntPtr priv, int use);

        /// Return Type: void
        ///priv: ASS_Renderer*
        ///par: double
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_set_pixel_aspect")]
        public static extern void ass_set_pixel_aspect(IntPtr priv, double par);

        /// Return Type: void
        ///priv: ASS_Renderer*
        ///dar: double
        ///sar: double
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_set_aspect_ratio")]
        public static extern void ass_set_aspect_ratio(IntPtr priv, double dar, double sar);

        /// Return Type: void
        ///priv: ASS_Renderer*
        ///font_scale: double
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_set_font_scale")]
        public static extern void ass_set_font_scale(IntPtr priv, double font_scale);

        /// Return Type: void
        ///priv: ASS_Renderer*
        ///ht: ASS_Hinting->Anonymous_50adfa8b_d6eb_47e5_984e_6ad569cb121f
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_set_hinting")]
        public static extern void ass_set_hinting(IntPtr priv, ASS_Hinting ht);

        /// Return Type: void
        ///priv: ASS_Renderer*
        ///line_spacing: double
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_set_line_spacing")]
        public static extern void ass_set_line_spacing(IntPtr priv, double line_spacing);

        /// Return Type: void
        ///priv: ASS_Renderer*
        ///line_position: double
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_set_line_position")]
        public static extern void ass_set_line_position(IntPtr priv, double line_position);

        /// Return Type: void
        ///priv: ASS_Library*
        ///providers: ASS_DefaultFontProvider**
        ///size: size_t*
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_get_available_font_providers")]
        public static extern void ass_get_available_font_providers(IntPtr priv, ref IntPtr providers, ref int size);

        /// Return Type: void
        ///priv: ASS_Renderer*
        ///default_font: char*
        ///default_family: char*
        ///dfp: int
        ///config: char*
        ///update: int
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_set_fonts")]
        public static extern void ass_set_fonts(IntPtr priv, IntPtr default_font, [System.Runtime.InteropServices.In()] [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)] string default_family, int dfp, IntPtr config, int update);

        /// Return Type: void
        ///priv: ASS_Renderer*
        ///bits: int
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_set_selective_style_override_enabled")]
        public static extern void ass_set_selective_style_override_enabled(IntPtr priv, int bits);

        /// Return Type: void
        ///priv: ASS_Renderer*
        ///style: ASS_Style*
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_set_selective_style_override")]
        public static extern void ass_set_selective_style_override(IntPtr priv, ref ASS_Style style);

        /// Return Type: int
        ///priv: ASS_Renderer*
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_fonts_update")]
        public static extern int ass_fonts_update(IntPtr priv);

        /// Return Type: void
        ///priv: ASS_Renderer*
        ///glyph_max: int
        ///bitmap_max_size: int
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_set_cache_limits")]
        public static extern void ass_set_cache_limits(IntPtr priv, int glyph_max, int bitmap_max_size);

        /// Return Type: ASS_Image*
        ///priv: ASS_Renderer*
        ///track: ASS_Track*
        ///now: int
        ///detect_change: int*
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_render_frame")]
        public static extern IntPtr ass_render_frame(IntPtr priv, IntPtr track, int now, ref int detect_change);

        /// Return Type: ASS_Track*
        ///param0: ASS_Library*
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_new_track")]
        public static extern IntPtr ass_new_track(IntPtr param0);

        /// Return Type: void
        ///track: ASS_Track*
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_free_track")]
        public static extern void ass_free_track(IntPtr track);

        /// Return Type: int
        ///track: ASS_Track*
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_alloc_style")]
        public static extern int ass_alloc_style(IntPtr track);

        /// Return Type: int
        ///track: ASS_Track*
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_alloc_event")]
        public static extern int ass_alloc_event(IntPtr track);

        /// Return Type: void
        ///track: ASS_Track*
        ///sid: int
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_free_style")]
        public static extern void ass_free_style(ref ASS_Track track, int sid);

        /// Return Type: void
        ///track: ASS_Track*
        ///eid: int
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_free_event")]
        public static extern void ass_free_event(ref ASS_Track track, int eid);

        /// Return Type: void
        ///track: ASS_Track*
        ///data: char*
        ///size: int
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_process_data")]
        public static extern void ass_process_data(ref ASS_Track track, IntPtr data, int size);

        /// Return Type: void
        ///track: ASS_Track*
        ///data: char*
        ///size: int
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_process_codec_private")]
        public static extern void ass_process_codec_private(ref ASS_Track track, IntPtr data, int size);

        /// Return Type: void
        ///track: ASS_Track*
        ///data: char*
        ///size: int
        ///timecode: int
        ///duration: int
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_process_chunk")]
        public static extern void ass_process_chunk(ref ASS_Track track, IntPtr data, int size, int timecode, int duration);

        /// Return Type: void
        ///track: ASS_Track*
        ///check_readorder: int
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_set_check_readorder")]
        public static extern void ass_set_check_readorder(ref ASS_Track track, int check_readorder);

        /// Return Type: void
        ///track: ASS_Track*
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_flush_events")]
        public static extern void ass_flush_events(ref ASS_Track track);

        /// Return Type: ASS_Track*
        ///library: ASS_Library*
        ///fname: char*
        ///codepage: char*
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_read_file")]
        public static extern IntPtr ass_read_file(IntPtr library, IntPtr fname, IntPtr codepage);

        /// Return Type: ASS_Track*
        ///library: ASS_Library*
        ///buf: char*
        ///bufsize: size_t->int
        ///codepage: char*
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_read_memory")]
        public static extern IntPtr ass_read_memory(IntPtr library, IntPtr buf, int bufsize, IntPtr codepage);

        /// Return Type: int
        ///track: ASS_Track*
        ///fname: char*
        ///codepage: char*
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_read_styles")]
        public static extern int ass_read_styles(ref ASS_Track track, IntPtr fname, IntPtr codepage);

        /// Return Type: void
        ///library: ASS_Library*
        ///name: char*
        ///data: char*
        ///data_size: int
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_add_font")]
        public static extern void ass_add_font(IntPtr library, IntPtr name, IntPtr data, int data_size);

        /// Return Type: void
        ///library: ASS_Library*
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_clear_fonts")]
        public static extern void ass_clear_fonts(IntPtr library);

        /// Return Type: int
        ///track: ASS_Track*
        ///now: int
        ///movement: int
        [System.Runtime.InteropServices.DllImport("libass.dll", EntryPoint = "ass_step_sub")]
        public static extern int ass_step_sub(ref ASS_Track track, int now, int movement);
    }
}
