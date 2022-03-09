using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace HBNiuBi.DM
{
    /// <summary>
    /// 大漠插件C#免注册调用类
    /// 作者：清风
    /// QQ：274838061
    /// 本模块必须包含dmc.dll 实现不用注册dm.dll 到系统可以动态调用
    /// 集成5.1423 和 6.1638 破解，仅供测试，请勿用于商业软件开发，请支持正版
    /// 注意：破解版创建对象的时候会破解内存，速度稍慢
    /// 最新修改：2017-12-4
    /// </summary>
    class CDmSoft : IDisposable
    {
        const string DMCNAME = "dmc.dll";
        #region import DLL 函数
        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr CreateDM(string dmpath);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FreeDM();

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetRowGapNoDict(IntPtr dm, int row_gap);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ReadFileData(IntPtr dm, string file_name, int start_pos, int end_pos);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SaveDict(IntPtr dm, int index, string file_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetDisplayInput(IntPtr dm, string mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FoobarClose(IntPtr dm, int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int CreateFolder(IntPtr dm, string folder_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int RightDown(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FaqCapture(IntPtr dm, int x1, int y1, int x2, int y2, int quality, int delay, int time);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int ClearDict(IntPtr dm, int index);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int WriteFloatAddr(IntPtr dm, int hwnd, int addr, float v);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWordLineHeightNoDict(IntPtr dm, int line_height);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindStrFastExS(IntPtr dm, int x1, int y1, int x2, int y2, string str, string color, double sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int MoveWindow(IntPtr dm, int hwnd, int x, int y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr EnumIniKeyPwd(IntPtr dm, string section, string file_name, string pwd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetExportDict(IntPtr dm, int index, string dict_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetTime(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr SortPosDistance(IntPtr dm, string all_pos, int tpe, int x, int y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int BindWindowEx(IntPtr dm, int hwnd, string display, string mouse, string keypad, string public_desc, int mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr EnumWindowByProcessId(IntPtr dm, int pid, string title, string class_name, int filter);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FaqCancel(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPointWindow(IntPtr dm, int x, int y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int CheckFontSmooth(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr OcrEx(IntPtr dm, int x1, int y1, int x2, int y2, string color, double sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int ExitOs(IntPtr dm, int tpe);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FindMulColor(IntPtr dm, int x1, int y1, int x2, int y2, string color, double sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindStrFastE(IntPtr dm, int x1, int y1, int x2, int y2, string str, string color, double sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetClipboard(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetMachineCode(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int EnableRealKeypad(IntPtr dm, int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindData(IntPtr dm, int hwnd, string addr_range, string data);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadInt(IntPtr dm, int hwnd, string addr, int tpe);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FaqRelease(IntPtr dm, int handle);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindStrWithFontEx(IntPtr dm, int x1, int y1, int x2, int y2, string str, string color, double sim, string font_name, int font_size, int flag);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetDict(IntPtr dm, int index, int font_index);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FaqCaptureFromFile(IntPtr dm, int x1, int y1, int x2, int y2, string file_name, int quality);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FindStrFast(IntPtr dm, int x1, int y1, int x2, int y2, string str, string color, double sim, out object x, out object y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int MoveDD(IntPtr dm, int dx, int dy);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr Md5(IntPtr dm, string str);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FaqGetSize(IntPtr dm, int handle);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FindWindow(IntPtr dm, string class_name, string title_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FindWindowSuper(IntPtr dm, string spec1, int flag1, int type1, string spec2, int flag2, int type2);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindStrExS(IntPtr dm, int x1, int y1, int x2, int y2, string str, string color, double sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int WheelDown(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetWindowProcessId(IntPtr dm, int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FoobarLock(IntPtr dm, int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int ForceUnBindWindow(IntPtr dm, int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int Stop(IntPtr dm, int id);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindString(IntPtr dm, int hwnd, string addr_range, string string_value, int tpe);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetExactOcr(IntPtr dm, int exact_ocr);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int DeleteIni(IntPtr dm, string section, string key, string file_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int AsmAdd(IntPtr dm, string asm_ins);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindInt(IntPtr dm, int hwnd, string addr_range, int int_value_min, int int_value_max, int tpe);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetDisplayDelay(IntPtr dm, int t);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetModuleBaseAddr(IntPtr dm, int hwnd, string module_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int KeyPressChar(IntPtr dm, string key_str);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetDictMem(IntPtr dm, int index, int addr, int size);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern double ReadDouble(IntPtr dm, int hwnd, string addr);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetScreenDataBmp(IntPtr dm, int x1, int y1, int x2, int y2, out object data, out object size);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int EnablePicCache(IntPtr dm, int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetDictCount(IntPtr dm, int index);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int CapturePng(IntPtr dm, int x1, int y1, int x2, int y2, string file_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetResultPos(IntPtr dm, string str, int index, out object x, out object y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FaqPost(IntPtr dm, string server, int handle, int request_type, int time_out);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindColorEx(IntPtr dm, int x1, int y1, int x2, int y2, string color, double sim, int dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetAero(IntPtr dm, int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int DisableScreenSave(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr MatchPicName(IntPtr dm, string pic_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int ShowScrMsg(IntPtr dm, int x1, int y1, int x2, int y2, string msg, string color);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetColor(IntPtr dm, int x, int y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int CopyFile(IntPtr dm, string src_file, string dst_file, int over);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int WriteString(IntPtr dm, int hwnd, string addr, int tpe, string v);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FoobarPrintText(IntPtr dm, int hwnd, string text, string color);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetDiskSerial(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetColGapNoDict(IntPtr dm, int col_gap);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr OcrInFile(IntPtr dm, int x1, int y1, int x2, int y2, string pic_name, string color, double sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWordLineHeight(IntPtr dm, int line_height);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetMinRowGap(IntPtr dm, int row_gap);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowTransparent(IntPtr dm, int hwnd, int v);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int DelEnv(IntPtr dm, int index, string name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int DownCpu(IntPtr dm, int rate);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWordGapNoDict(IntPtr dm, int word_gap);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ReadIniPwd(IntPtr dm, string section, string key, string file_name, string pwd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetScreenHeight(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FindStr(IntPtr dm, int x1, int y1, int x2, int y2, string str, string color, double sim, out object x, out object y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int VirtualProtectEx(IntPtr dm, int hwnd, int addr, int size, int tpe, int old_protect);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetOsType(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int VirtualFreeEx(IntPtr dm, int hwnd, int addr);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int EnableKeypadSync(IntPtr dm, int en, int time_out);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetDictInfo(IntPtr dm, string str, string font_name, int font_size, int flag);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindStrEx(IntPtr dm, int x1, int y1, int x2, int y2, string str, string color, double sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int RegExNoMac(IntPtr dm, string code, string Ver, string ip);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr EnumWindowSuper(IntPtr dm, string spec1, int flag1, int type1, string spec2, int flag2, int type2, int sort);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetLastError(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int DeleteFolder(IntPtr dm, string folder_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindStrFastEx(IntPtr dm, int x1, int y1, int x2, int y2, string str, string color, double sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int LeftUp(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetColorHSV(IntPtr dm, int x, int y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int EnableSpeedDx(IntPtr dm, int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int CreateFoobarRect(IntPtr dm, int hwnd, int x, int y, int w, int h);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int ActiveInputMethod(IntPtr dm, int hwnd, string id);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindPicEx(IntPtr dm, int x1, int y1, int x2, int y2, string pic_name, string delta_color, double sim, int dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetClientSize(IntPtr dm, int hwnd, int width, int height);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindPicExS(IntPtr dm, int x1, int y1, int x2, int y2, string pic_name, string delta_color, double sim, int dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int EnableMouseSync(IntPtr dm, int en, int time_out);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr MoveToEx(IntPtr dm, int x, int y, int w, int h);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int WriteFloat(IntPtr dm, int hwnd, string addr, float v);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetWindow(IntPtr dm, int hwnd, int flag);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int LoadPicByte(IntPtr dm, int addr, int size, string name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetCursorShapeEx(IntPtr dm, int tpe);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int WaitKey(IntPtr dm, int key_code, int time_out);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetKeyState(IntPtr dm, int vk);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int WriteIniPwd(IntPtr dm, string section, string key, string v, string file_name, string pwd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FoobarStartGif(IntPtr dm, int hwnd, int x, int y, string pic_name, int repeat_limit, int delay);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FaqCaptureString(IntPtr dm, string str);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int MoveR(IntPtr dm, int rx, int ry);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FoobarTextPrintDir(IntPtr dm, int hwnd, int dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetInfo(IntPtr dm, string cmd, string param);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int KeyUp(IntPtr dm, int vk);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetMachineCodeNoMac(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindColorE(IntPtr dm, int x1, int y1, int x2, int y2, string color, double sim, int dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SendString(IntPtr dm, int hwnd, string str);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetMemoryHwndAsProcessId(IntPtr dm, int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int VirtualAllocEx(IntPtr dm, int hwnd, int addr, int size, int tpe);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindDouble(IntPtr dm, int hwnd, string addr_range, double double_value_min, double double_value_max);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetClientSize(IntPtr dm, int hwnd, out object width, out object height);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int KeyDownChar(IntPtr dm, string key_str);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindStrE(IntPtr dm, int x1, int y1, int x2, int y2, string str, string color, double sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindPicS(IntPtr dm, int x1, int y1, int x2, int y2, string pic_name, string delta_color, double sim, int dir, out object x, out object y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetColorBGR(IntPtr dm, int x, int y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int WheelUp(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ReadDataAddr(IntPtr dm, int hwnd, int addr, int length);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr EnumIniSectionPwd(IntPtr dm, string file_name, string pwd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SendCommand(IntPtr dm, string cmd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int KeyDown(IntPtr dm, int vk);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int EnableFakeActive(IntPtr dm, int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int MiddleDown(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetScreen(IntPtr dm, int width, int height, int depth);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr Ver(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SendStringIme(IntPtr dm, string str);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int Log(IntPtr dm, string info);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr Ocr(IntPtr dm, int x1, int y1, int x2, int y2, string color, double sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FindColor(IntPtr dm, int x1, int y1, int x2, int y2, string color, double sim, int dir, out object x, out object y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int UseDict(IntPtr dm, int index);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int MiddleUp(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindColorBlockEx(IntPtr dm, int x1, int y1, int x2, int y2, string color, double sim, int count, int width, int height);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FoobarDrawLine(IntPtr dm, int hwnd, int x1, int y1, int x2, int y2, string color, int style, int width);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetMemoryFindResultToFile(IntPtr dm, string file_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int EnableFontSmooth(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetAveHSV(IntPtr dm, int x1, int y1, int x2, int y2);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FaqFetch(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int StrStr(IntPtr dm, string s, string str);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetWordsNoDict(IntPtr dm, int x1, int y1, int x2, int y2, string color);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr StringToData(IntPtr dm, string string_value, int tpe);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetSimMode(IntPtr dm, int mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowText(IntPtr dm, int hwnd, string text);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int DisableFontSmooth(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int LeaveCri(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetCursorSpot(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int CmpColor(IntPtr dm, int x, int y, string color, double sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int UnBindWindow(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FindMultiColor(IntPtr dm, int x1, int y1, int x2, int y2, string first_color, string offset_color, double sim, int dir, out object x, out object y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int WriteInt(IntPtr dm, int hwnd, string addr, int tpe, int v);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SendPaste(IntPtr dm, int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowState(IntPtr dm, int hwnd, int flag);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr DoubleToData(IntPtr dm, double double_value);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int RunApp(IntPtr dm, string path, int mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetPicPwd(IntPtr dm, string pwd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetNowDict(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindMultiColorE(IntPtr dm, int x1, int y1, int x2, int y2, string first_color, string offset_color, double sim, int dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr EnumProcess(IntPtr dm, string name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int LeftDoubleClick(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetFileLength(IntPtr dm, string file_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int EnableKeypadMsg(IntPtr dm, int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int DisablePowerSave(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int CreateFoobarCustom(IntPtr dm, int hwnd, int x, int y, string pic, string trans_color, double sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetWordResultCount(IntPtr dm, string str);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int LockInput(IntPtr dm, int locks);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int MoveTo(IntPtr dm, int x, int y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int EnableBind(IntPtr dm, int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindMultiColorEx(IntPtr dm, int x1, int y1, int x2, int y2, string first_color, string offset_color, double sim, int dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetResultCount(IntPtr dm, string str);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetProcessInfo(IntPtr dm, int pid);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetPath(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int LeftDown(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int MoveFile(IntPtr dm, string src_file, string dst_file);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWordGap(IntPtr dm, int word_gap);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int DeleteFile(IntPtr dm, string file_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int BindWindow(IntPtr dm, int hwnd, string display, string mouse, string keypad, int mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int EnableMouseAccuracy(IntPtr dm, int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindDoubleEx(IntPtr dm, int hwnd, string addr_range, double double_value_min, double double_value_max, int steps, int multi_thread, int mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetCursorShape(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetWordResultStr(IntPtr dm, string str, int index);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern float ReadFloat(IntPtr dm, int hwnd, string addr);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int EnableDisplayDebug(IntPtr dm, int enable_debug);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int LockMouseRect(IntPtr dm, int x1, int y1, int x2, int y2);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int WriteDouble(IntPtr dm, int hwnd, string addr, double v);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FoobarFillRect(IntPtr dm, int hwnd, int x1, int y1, int x2, int y2, string color);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int DecodeFile(IntPtr dm, string file_name, string pwd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FetchWord(IntPtr dm, int x1, int y1, int x2, int y2, string color, string word);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindStringEx(IntPtr dm, int hwnd, string addr_range, string string_value, int tpe, int steps, int multi_thread, int mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int LoadPic(IntPtr dm, string pic_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int EncodeFile(IntPtr dm, string file_name, string pwd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ReadStringAddr(IntPtr dm, int hwnd, int addr, int tpe, int length);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int CheckInputMethod(IntPtr dm, int hwnd, string id);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int IsFileExist(IntPtr dm, string file_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetDictPwd(IntPtr dm, string pwd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FindWindowByProcess(IntPtr dm, string process_name, string class_name, string title_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetMouseSpeed(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetSpecialWindow(IntPtr dm, int flag);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int Is64Bit(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetDir(IntPtr dm, int tpe);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SwitchBindWindow(IntPtr dm, int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindStrWithFontE(IntPtr dm, int x1, int y1, int x2, int y2, string str, string color, double sim, string font_name, int font_size, int flag);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int InitCri(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int Delays(IntPtr dm, int min_s, int max_s);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int CaptureGif(IntPtr dm, int x1, int y1, int x2, int y2, string file_name, int delay, int time);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindNearestPos(IntPtr dm, string all_pos, int tpe, int x, int y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int AsmCall(IntPtr dm, int hwnd, int mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int CreateFoobarEllipse(IntPtr dm, int hwnd, int x, int y, int w, int h);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int WriteIntAddr(IntPtr dm, int hwnd, int addr, int tpe, int v);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetDisplayInfo(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetWords(IntPtr dm, int x1, int y1, int x2, int y2, string color, double sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SendString2(IntPtr dm, int hwnd, string str);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetScreenWidth(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FoobarUpdate(IntPtr dm, int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetEnv(IntPtr dm, int index, string name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int CheckUAC(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetDict(IntPtr dm, int index, string dict_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int AddDict(IntPtr dm, int index, string dict_info);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int DmGuard(IntPtr dm, int en, string tpe);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FindStrWithFont(IntPtr dm, int x1, int y1, int x2, int y2, string str, string color, double sim, string font_name, int font_size, int flag, out object x, out object y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern double ReadDoubleAddr(IntPtr dm, int hwnd, int addr);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FindInputMethod(IntPtr dm, string id);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int DeleteIniPwd(IntPtr dm, string section, string key, string file_name, string pwd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetID(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetUAC(IntPtr dm, int uac);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int OpenProcess(IntPtr dm, int pid);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetPath(IntPtr dm, string path);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetForegroundFocus(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int EnterCri(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindShapeE(IntPtr dm, int x1, int y1, int x2, int y2, string offset_color, double sim, int dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindPicMemEx(IntPtr dm, int x1, int y1, int x2, int y2, string pic_info, string delta_color, double sim, int dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetWordResultPos(IntPtr dm, string str, int index, out object x, out object y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetShowErrorMsg(IntPtr dm, int show);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FindPic(IntPtr dm, int x1, int y1, int x2, int y2, string pic_name, string delta_color, double sim, int dir, out object x, out object y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int LeftClick(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int WriteStringAddr(IntPtr dm, int hwnd, int addr, int tpe, string v);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int EnableKeypadPatch(IntPtr dm, int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowSize(IntPtr dm, int hwnd, int width, int height);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetAveRGB(IntPtr dm, int x1, int y1, int x2, int y2);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetWindowState(IntPtr dm, int hwnd, int flag);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int AsmClear(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr IntToData(IntPtr dm, int int_value, int tpe);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FindShape(IntPtr dm, int x1, int y1, int x2, int y2, string offset_color, double sim, int dir, out object x, out object y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int DownloadFile(IntPtr dm, string url, string save_file, int timeout);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int Reg(IntPtr dm, string code, string Ver);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetMinColGap(IntPtr dm, int col_gap);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int EnableRealMouse(IntPtr dm, int en, int mousedelay, int mousestep);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr BGR2RGB(IntPtr dm, string bgr_color);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FoobarSetSave(IntPtr dm, int hwnd, string file_name, int en, string header);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FoobarClearText(IntPtr dm, int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int delay(IntPtr dm, int mis);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr SelectFile(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int KeyPress(IntPtr dm, int vk);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetEnumWindowDelay(IntPtr dm, int delay);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int Play(IntPtr dm, string file_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ReadData(IntPtr dm, int hwnd, string addr, int length);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetKeypadDelay(IntPtr dm, string tpe, int delay);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FindWindowEx(IntPtr dm, int parent, string class_name, string title_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetMouseSpeed(IntPtr dm, int speed);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FreeProcessMemory(IntPtr dm, int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FoobarSetFont(IntPtr dm, int hwnd, string font_name, int size, int flag);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FoobarDrawPic(IntPtr dm, int hwnd, int x, int y, string pic, string trans_color);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int Capture(IntPtr dm, int x1, int y1, int x2, int y2, string file_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetWindowProcessPath(IntPtr dm, int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetCursorPos(IntPtr dm, out object x, out object y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FindPicMem(IntPtr dm, int x1, int y1, int x2, int y2, string pic_info, string delta_color, double sim, int dir, out object x, out object y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int TerminateProcess(IntPtr dm, int pid);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int Beep(IntPtr dm, int fre, int delay);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr EnumWindow(IntPtr dm, int parent, string title, string class_name, int filter);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindFloatEx(IntPtr dm, int hwnd, string addr_range, float float_value_min, float float_value_max, int steps, int multi_thread, int mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr OcrExOne(IntPtr dm, int x1, int y1, int x2, int y2, string color, double sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int RegEx(IntPtr dm, string code, string Ver, string ip);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetCommandLine(IntPtr dm, int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindShapeEx(IntPtr dm, int x1, int y1, int x2, int y2, string offset_color, double sim, int dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern float ReadFloatAddr(IntPtr dm, int hwnd, int addr);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetWindowTitle(IntPtr dm, int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr EnumWindowByProcess(IntPtr dm, string process_name, string title, string class_name, int filter);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FoobarTextRect(IntPtr dm, int hwnd, int x, int y, int w, int h);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetWindowClass(IntPtr dm, int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr RGB2BGR(IntPtr dm, string rgb_color);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FindWindowByProcessId(IntPtr dm, int process_id, string class_name, string title_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetNetTime(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int KeyPressStr(IntPtr dm, string key_str, int delay);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetColorNum(IntPtr dm, int x1, int y1, int x2, int y2, string color, double sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr AsmCode(IntPtr dm, int base_addr);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int WriteData(IntPtr dm, int hwnd, string addr, string data);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FoobarDrawText(IntPtr dm, int hwnd, int x, int y, int w, int h, string text, string color, int align);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int IsDisplayDead(IntPtr dm, int x1, int y1, int x2, int y2, int t);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int EnableGetColorByCapture(IntPtr dm, int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetWindowRect(IntPtr dm, int hwnd, out object x1, out object y1, out object x2, out object y2);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr Assemble(IntPtr dm, string asm_code, int base_addr, int is_upper);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FreePic(IntPtr dm, string pic_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetPicSize(IntPtr dm, string pic_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FindColorBlock(IntPtr dm, int x1, int y1, int x2, int y2, string color, double sim, int count, int width, int height, out object x, out object y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FreeScreenData(IntPtr dm, int handle);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int CaptureJpg(IntPtr dm, int x1, int y1, int x2, int y2, string file_name, int quality);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int WriteDataAddr(IntPtr dm, int hwnd, int addr, string data);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SendStringIme2(IntPtr dm, int hwnd, string str, int mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetEnv(IntPtr dm, int index, string name, string value);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr SelectDirectory(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindStrS(IntPtr dm, int x1, int y1, int x2, int y2, string str, string color, double sim, out object x, out object y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindIntEx(IntPtr dm, int hwnd, string addr_range, int int_value_min, int int_value_max, int tpe, int steps, int multi_thread, int mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int EnableMouseMsg(IntPtr dm, int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr AppendPicAddr(IntPtr dm, string pic_info, int addr, int size);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int CreateFoobarRoundRect(IntPtr dm, int hwnd, int x, int y, int w, int h, int rw, int rh);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetForegroundWindow(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int LockDisplay(IntPtr dm, int locks);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetDisplayAcceler(IntPtr dm, int level);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ExcludePos(IntPtr dm, string all_pos, int tpe, int x1, int y1, int x2, int y2);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int WriteFile(IntPtr dm, string file_name, string content);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetBasePath(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetNetTimeByIp(IntPtr dm, string ip);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FoobarStopGif(IntPtr dm, int hwnd, int x, int y, string pic_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ReadFile(IntPtr dm, string file_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int EnableIme(IntPtr dm, int en);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int RegNoMac(IntPtr dm, string code, string Ver);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindStrFastS(IntPtr dm, int x1, int y1, int x2, int y2, string str, string color, double sim, out object x, out object y);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FoobarUnlock(IntPtr dm, int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetMouseDelay(IntPtr dm, string tpe, int delay);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int ImageToBmp(IntPtr dm, string pic_name, string bmp_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetScreenData(IntPtr dm, int x1, int y1, int x2, int y2);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetMousePointWindow(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadIntAddr(IntPtr dm, int hwnd, int addr, int tpe);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int WriteIni(IntPtr dm, string section, string key, string v, string file_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetClipboard(IntPtr dm, string data);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindDataEx(IntPtr dm, int hwnd, string addr_range, string data, int steps, int multi_thread, int mode);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetNetTimeSafe(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindPicMemE(IntPtr dm, int x1, int y1, int x2, int y2, string pic_info, string delta_color, double sim, int dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FloatToData(IntPtr dm, float float_value);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FoobarSetTrans(IntPtr dm, int hwnd, int trans, string color, double sim);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int CapturePre(IntPtr dm, string file_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr EnumIniKey(IntPtr dm, string section, string file_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int KeyUpChar(IntPtr dm, string key_str);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int WriteDoubleAddr(IntPtr dm, int hwnd, int addr, double v);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetClientRect(IntPtr dm, int hwnd, out object x1, out object y1, out object x2, out object y2);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int MiddleClick(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetScreenDepth(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindPicE(IntPtr dm, int x1, int y1, int x2, int y2, string pic_name, string delta_color, double sim, int dir);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FaqIsPosted(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ReadString(IntPtr dm, int hwnd, string addr, int tpe, int length);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetMac(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetBindWindow(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int RightUp(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindFloat(IntPtr dm, int hwnd, string addr_range, float float_value_min, float float_value_max);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ReadIni(IntPtr dm, string section, string key, string file_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr EnumIniSection(IntPtr dm, string file_name);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int FoobarTextLineGap(IntPtr dm, int hwnd, int gap);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetDmCount(IntPtr dm);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int IsBind(IntPtr dm, int hwnd);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FaqSend(IntPtr dm, string server, int handle, int request_type, int time_out);

        [DllImport(DMCNAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int RightClick(IntPtr dm);






        #endregion

        private IntPtr _dm = IntPtr.Zero;
        private bool disposed = false;

        public IntPtr DM
        {
            get { return _dm; }
            set { _dm = value; }
        }


        public CDmSoft()
        {
            _dm = CreateDM("dm.dll");
        }

        public CDmSoft(string path)
        {
            _dm = CreateDM(path);
        }

        public int SetRowGapNoDict(int row_gap)
        {
            return SetRowGapNoDict(_dm, row_gap);

        }

        public string ReadFileData(string file_name, int start_pos, int end_pos)
        {
            return Marshal.PtrToStringUni(ReadFileData(_dm, file_name, start_pos, end_pos));

        }

        public int SaveDict(int index, string file_name)
        {
            return SaveDict(_dm, index, file_name);

        }

        public int SetDisplayInput(string mode)
        {
            return SetDisplayInput(_dm, mode);

        }

        public int FoobarClose(int hwnd)
        {
            return FoobarClose(_dm, hwnd);

        }

        public int CreateFolder(string folder_name)
        {
            return CreateFolder(_dm, folder_name);

        }

        public int RightDown()
        {
            return RightDown(_dm);

        }

        public int FaqCapture(int x1, int y1, int x2, int y2, int quality, int delay, int time)
        {
            return FaqCapture(_dm, x1, y1, x2, y2, quality, delay, time);

        }

        public int ClearDict(int index)
        {
            return ClearDict(_dm, index);

        }

        public int WriteFloatAddr(int hwnd, int addr, float v)
        {
            return WriteFloatAddr(_dm, hwnd, addr, v);

        }

        public int SetWordLineHeightNoDict(int line_height)
        {
            return SetWordLineHeightNoDict(_dm, line_height);

        }

        public string FindStrFastExS(int x1, int y1, int x2, int y2, string str, string color, double sim)
        {
            return Marshal.PtrToStringUni(FindStrFastExS(_dm, x1, y1, x2, y2, str, color, sim));

        }

        public int MoveWindow(int hwnd, int x, int y)
        {
            return MoveWindow(_dm, hwnd, x, y);

        }

        public string EnumIniKeyPwd(string section, string file_name, string pwd)
        {
            return Marshal.PtrToStringUni(EnumIniKeyPwd(_dm, section, file_name, pwd));

        }

        public int SetExportDict(int index, string dict_name)
        {
            return SetExportDict(_dm, index, dict_name);

        }

        public int GetTime()
        {
            return GetTime(_dm);

        }

        public string SortPosDistance(string all_pos, int tpe, int x, int y)
        {
            return Marshal.PtrToStringUni(SortPosDistance(_dm, all_pos, tpe, x, y));

        }

        public int BindWindowEx(int hwnd, string display, string mouse, string keypad, string public_desc, int mode)
        {
            return BindWindowEx(_dm, hwnd, display, mouse, keypad, public_desc, mode);

        }

        public string EnumWindowByProcessId(int pid, string title, string class_name, int filter)
        {
            return Marshal.PtrToStringUni(EnumWindowByProcessId(_dm, pid, title, class_name, filter));

        }

        public int FaqCancel()
        {
            return FaqCancel(_dm);

        }

        public int GetPointWindow(int x, int y)
        {
            return GetPointWindow(_dm, x, y);

        }

        public int CheckFontSmooth()
        {
            return CheckFontSmooth(_dm);

        }

        public string OcrEx(int x1, int y1, int x2, int y2, string color, double sim)
        {
            return Marshal.PtrToStringUni(OcrEx(_dm, x1, y1, x2, y2, color, sim));

        }

        public int ExitOs(int tpe)
        {
            return ExitOs(_dm, tpe);

        }

        public int FindMulColor(int x1, int y1, int x2, int y2, string color, double sim)
        {
            return FindMulColor(_dm, x1, y1, x2, y2, color, sim);

        }

        public string FindStrFastE(int x1, int y1, int x2, int y2, string str, string color, double sim)
        {
            return Marshal.PtrToStringUni(FindStrFastE(_dm, x1, y1, x2, y2, str, color, sim));

        }

        public string GetClipboard()
        {
            return Marshal.PtrToStringUni(GetClipboard(_dm));

        }

        public string GetMachineCode()
        {
            return Marshal.PtrToStringUni(GetMachineCode(_dm));

        }

        public int EnableRealKeypad(int en)
        {
            return EnableRealKeypad(_dm, en);

        }

        public string FindData(int hwnd, string addr_range, string data)
        {
            return Marshal.PtrToStringUni(FindData(_dm, hwnd, addr_range, data));

        }

        public int ReadInt(int hwnd, string addr, int tpe)
        {
            return ReadInt(_dm, hwnd, addr, tpe);

        }

        public int FaqRelease(int handle)
        {
            return FaqRelease(_dm, handle);

        }

        public string FindStrWithFontEx(int x1, int y1, int x2, int y2, string str, string color, double sim, string font_name, int font_size, int flag)
        {
            return Marshal.PtrToStringUni(FindStrWithFontEx(_dm, x1, y1, x2, y2, str, color, sim, font_name, font_size, flag));

        }

        public string GetDict(int index, int font_index)
        {
            return Marshal.PtrToStringUni(GetDict(_dm, index, font_index));

        }

        public int FaqCaptureFromFile(int x1, int y1, int x2, int y2, string file_name, int quality)
        {
            return FaqCaptureFromFile(_dm, x1, y1, x2, y2, file_name, quality);

        }

        public int FindStrFast(int x1, int y1, int x2, int y2, string str, string color, double sim, out int x, out int y)
        {
            object ox;
            object oy;
            int result = FindStrFast(_dm, x1, y1, x2, y2, str, color, sim, out ox, out oy);
            x = (int)ox;
            y = (int)oy;
            return result;

        }

        public int MoveDD(int dx, int dy)
        {
            return MoveDD(_dm, dx, dy);

        }

        public string Md5(string str)
        {
            return Marshal.PtrToStringUni(Md5(_dm, str));

        }

        public int FaqGetSize(int handle)
        {
            return FaqGetSize(_dm, handle);

        }

        public int FindWindow(string class_name, string title_name)
        {
            return FindWindow(_dm, class_name, title_name);

        }

        public int FindWindowSuper(string spec1, int flag1, int type1, string spec2, int flag2, int type2)
        {
            return FindWindowSuper(_dm, spec1, flag1, type1, spec2, flag2, type2);

        }

        public string FindStrExS(int x1, int y1, int x2, int y2, string str, string color, double sim)
        {
            return Marshal.PtrToStringUni(FindStrExS(_dm, x1, y1, x2, y2, str, color, sim));

        }

        public int WheelDown()
        {
            return WheelDown(_dm);

        }

        public int GetWindowProcessId(int hwnd)
        {
            return GetWindowProcessId(_dm, hwnd);

        }

        public int FoobarLock(int hwnd)
        {
            return FoobarLock(_dm, hwnd);

        }

        public int ForceUnBindWindow(int hwnd)
        {
            return ForceUnBindWindow(_dm, hwnd);

        }

        public int Stop(int id)
        {
            return Stop(_dm, id);

        }

        public string FindString(int hwnd, string addr_range, string string_value, int tpe)
        {
            return Marshal.PtrToStringUni(FindString(_dm, hwnd, addr_range, string_value, tpe));

        }

        public int SetExactOcr(int exact_ocr)
        {
            return SetExactOcr(_dm, exact_ocr);

        }

        public int DeleteIni(string section, string key, string file_name)
        {
            return DeleteIni(_dm, section, key, file_name);

        }

        public int AsmAdd(string asm_ins)
        {
            return AsmAdd(_dm, asm_ins);

        }

        public string FindInt(int hwnd, string addr_range, int int_value_min, int int_value_max, int tpe)
        {
            return Marshal.PtrToStringUni(FindInt(_dm, hwnd, addr_range, int_value_min, int_value_max, tpe));

        }

        public int SetDisplayDelay(int t)
        {
            return SetDisplayDelay(_dm, t);

        }

        public int GetModuleBaseAddr(int hwnd, string module_name)
        {
            return GetModuleBaseAddr(_dm, hwnd, module_name);

        }

        public int KeyPressChar(string key_str)
        {
            return KeyPressChar(_dm, key_str);

        }

        public int SetDictMem(int index, int addr, int size)
        {
            return SetDictMem(_dm, index, addr, size);

        }

        public double ReadDouble(int hwnd, string addr)
        {
            return ReadDouble(_dm, hwnd, addr);

        }

        public int GetScreenDataBmp(int x1, int y1, int x2, int y2, out int data, out int size)
        {
            object odata;
            object osize;
            int result = GetScreenDataBmp(_dm, x1, y1, x2, y2, out odata, out osize);
            data = (int)odata;
            size = (int)osize;
            return result;

        }

        public int EnablePicCache(int en)
        {
            return EnablePicCache(_dm, en);

        }

        public int GetDictCount(int index)
        {
            return GetDictCount(_dm, index);

        }

        public int CapturePng(int x1, int y1, int x2, int y2, string file_name)
        {
            return CapturePng(_dm, x1, y1, x2, y2, file_name);

        }

        public int GetResultPos(string str, int index, out int x, out int y)
        {
            object ox;
            object oy;
            int result = GetResultPos(_dm, str, index, out ox, out oy);
            x = (int)ox;
            y = (int)oy;
            return result;

        }

        public int FaqPost(string server, int handle, int request_type, int time_out)
        {
            return FaqPost(_dm, server, handle, request_type, time_out);

        }

        public string FindColorEx(int x1, int y1, int x2, int y2, string color, double sim, int dir)
        {
            return Marshal.PtrToStringUni(FindColorEx(_dm, x1, y1, x2, y2, color, sim, dir));

        }

        public int SetAero(int en)
        {
            return SetAero(_dm, en);

        }

        public int DisableScreenSave()
        {
            return DisableScreenSave(_dm);

        }

        public string MatchPicName(string pic_name)
        {
            return Marshal.PtrToStringUni(MatchPicName(_dm, pic_name));

        }

        public int ShowScrMsg(int x1, int y1, int x2, int y2, string msg, string color)
        {
            return ShowScrMsg(_dm, x1, y1, x2, y2, msg, color);

        }

        public string GetColor(int x, int y)
        {
            return Marshal.PtrToStringUni(GetColor(_dm, x, y));

        }

        public int CopyFile(string src_file, string dst_file, int over)
        {
            return CopyFile(_dm, src_file, dst_file, over);

        }

        public int WriteString(int hwnd, string addr, int tpe, string v)
        {
            return WriteString(_dm, hwnd, addr, tpe, v);

        }

        public int FoobarPrintText(int hwnd, string text, string color)
        {
            return FoobarPrintText(_dm, hwnd, text, color);

        }

        public string GetDiskSerial()
        {
            return Marshal.PtrToStringUni(GetDiskSerial(_dm));

        }

        public int SetColGapNoDict(int col_gap)
        {
            return SetColGapNoDict(_dm, col_gap);

        }

        public string OcrInFile(int x1, int y1, int x2, int y2, string pic_name, string color, double sim)
        {
            return Marshal.PtrToStringUni(OcrInFile(_dm, x1, y1, x2, y2, pic_name, color, sim));

        }

        public int SetWordLineHeight(int line_height)
        {
            return SetWordLineHeight(_dm, line_height);

        }

        public int SetMinRowGap(int row_gap)
        {
            return SetMinRowGap(_dm, row_gap);

        }

        public int SetWindowTransparent(int hwnd, int v)
        {
            return SetWindowTransparent(_dm, hwnd, v);

        }

        public int DelEnv(int index, string name)
        {
            return DelEnv(_dm, index, name);

        }

        public int DownCpu(int rate)
        {
            return DownCpu(_dm, rate);

        }

        public int SetWordGapNoDict(int word_gap)
        {
            return SetWordGapNoDict(_dm, word_gap);

        }

        public string ReadIniPwd(string section, string key, string file_name, string pwd)
        {
            return Marshal.PtrToStringUni(ReadIniPwd(_dm, section, key, file_name, pwd));

        }

        public int GetScreenHeight()
        {
            return GetScreenHeight(_dm);

        }

        public int FindStr(int x1, int y1, int x2, int y2, string str, string color, double sim, out int x, out int y)
        {
            object ox;
            object oy;
            int result = FindStr(_dm, x1, y1, x2, y2, str, color, sim, out ox, out oy);
            x = (int)ox;
            y = (int)oy;
            return result;

        }

        public int VirtualProtectEx(int hwnd, int addr, int size, int tpe, int old_protect)
        {
            return VirtualProtectEx(_dm, hwnd, addr, size, tpe, old_protect);

        }

        public int GetOsType()
        {
            return GetOsType(_dm);

        }

        public int VirtualFreeEx(int hwnd, int addr)
        {
            return VirtualFreeEx(_dm, hwnd, addr);

        }

        public int EnableKeypadSync(int en, int time_out)
        {
            return EnableKeypadSync(_dm, en, time_out);

        }

        public string GetDictInfo(string str, string font_name, int font_size, int flag)
        {
            return Marshal.PtrToStringUni(GetDictInfo(_dm, str, font_name, font_size, flag));

        }

        public string FindStrEx(int x1, int y1, int x2, int y2, string str, string color, double sim)
        {
            return Marshal.PtrToStringUni(FindStrEx(_dm, x1, y1, x2, y2, str, color, sim));

        }

        public int RegExNoMac(string code, string Ver, string ip)
        {
            return RegExNoMac(_dm, code, Ver, ip);

        }

        public string EnumWindowSuper(string spec1, int flag1, int type1, string spec2, int flag2, int type2, int sort)
        {
            return Marshal.PtrToStringUni(EnumWindowSuper(_dm, spec1, flag1, type1, spec2, flag2, type2, sort));

        }

        public int GetLastError()
        {
            return GetLastError(_dm);

        }

        public int DeleteFolder(string folder_name)
        {
            return DeleteFolder(_dm, folder_name);

        }

        public string FindStrFastEx(int x1, int y1, int x2, int y2, string str, string color, double sim)
        {
            return Marshal.PtrToStringUni(FindStrFastEx(_dm, x1, y1, x2, y2, str, color, sim));

        }

        public int LeftUp()
        {
            return LeftUp(_dm);

        }

        public string GetColorHSV(int x, int y)
        {
            return Marshal.PtrToStringUni(GetColorHSV(_dm, x, y));

        }

        public int EnableSpeedDx(int en)
        {
            return EnableSpeedDx(_dm, en);

        }

        public int CreateFoobarRect(int hwnd, int x, int y, int w, int h)
        {
            return CreateFoobarRect(_dm, hwnd, x, y, w, h);

        }

        public int ActiveInputMethod(int hwnd, string id)
        {
            return ActiveInputMethod(_dm, hwnd, id);

        }

        public string FindPicEx(int x1, int y1, int x2, int y2, string pic_name, string delta_color, double sim, int dir)
        {
            return Marshal.PtrToStringUni(FindPicEx(_dm, x1, y1, x2, y2, pic_name, delta_color, sim, dir));

        }

        public int SetClientSize(int hwnd, int width, int height)
        {
            return SetClientSize(_dm, hwnd, width, height);

        }

        public string FindPicExS(int x1, int y1, int x2, int y2, string pic_name, string delta_color, double sim, int dir)
        {
            return Marshal.PtrToStringUni(FindPicExS(_dm, x1, y1, x2, y2, pic_name, delta_color, sim, dir));

        }

        public int EnableMouseSync(int en, int time_out)
        {
            return EnableMouseSync(_dm, en, time_out);

        }

        public string MoveToEx(int x, int y, int w, int h)
        {
            return Marshal.PtrToStringUni(MoveToEx(_dm, x, y, w, h));

        }

        public int WriteFloat(int hwnd, string addr, float v)
        {
            return WriteFloat(_dm, hwnd, addr, v);

        }

        public int GetWindow(int hwnd, int flag)
        {
            return GetWindow(_dm, hwnd, flag);

        }

        public int LoadPicByte(int addr, int size, string name)
        {
            return LoadPicByte(_dm, addr, size, name);

        }

        public string GetCursorShapeEx(int tpe)
        {
            return Marshal.PtrToStringUni(GetCursorShapeEx(_dm, tpe));

        }

        public int WaitKey(int key_code, int time_out)
        {
            return WaitKey(_dm, key_code, time_out);

        }

        public int GetKeyState(int vk)
        {
            return GetKeyState(_dm, vk);

        }

        public int WriteIniPwd(string section, string key, string v, string file_name, string pwd)
        {
            return WriteIniPwd(_dm, section, key, v, file_name, pwd);

        }

        public int FoobarStartGif(int hwnd, int x, int y, string pic_name, int repeat_limit, int delay)
        {
            return FoobarStartGif(_dm, hwnd, x, y, pic_name, repeat_limit, delay);

        }

        public int FaqCaptureString(string str)
        {
            return FaqCaptureString(_dm, str);

        }

        public int MoveR(int rx, int ry)
        {
            return MoveR(_dm, rx, ry);

        }

        public int FoobarTextPrintDir(int hwnd, int dir)
        {
            return FoobarTextPrintDir(_dm, hwnd, dir);

        }

        public string GetInfo(string cmd, string param)
        {
            return Marshal.PtrToStringUni(GetInfo(_dm, cmd, param));

        }

        public int KeyUp(int vk)
        {
            return KeyUp(_dm, vk);

        }

        public string GetMachineCodeNoMac()
        {
            return Marshal.PtrToStringUni(GetMachineCodeNoMac(_dm));

        }

        public string FindColorE(int x1, int y1, int x2, int y2, string color, double sim, int dir)
        {
            return Marshal.PtrToStringUni(FindColorE(_dm, x1, y1, x2, y2, color, sim, dir));

        }

        public int SendString(int hwnd, string str)
        {
            return SendString(_dm, hwnd, str);

        }

        public int SetMemoryHwndAsProcessId(int en)
        {
            return SetMemoryHwndAsProcessId(_dm, en);

        }

        public int VirtualAllocEx(int hwnd, int addr, int size, int tpe)
        {
            return VirtualAllocEx(_dm, hwnd, addr, size, tpe);

        }

        public string FindDouble(int hwnd, string addr_range, double double_value_min, double double_value_max)
        {
            return Marshal.PtrToStringUni(FindDouble(_dm, hwnd, addr_range, double_value_min, double_value_max));

        }

        public int GetClientSize(int hwnd, out int width, out int height)
        {
            object owidth;
            object oheight;
            int result = GetClientSize(_dm, hwnd, out owidth, out oheight);
            width = (int)owidth;
            height = (int)oheight;
            return result;

        }

        public int KeyDownChar(string key_str)
        {
            return KeyDownChar(_dm, key_str);

        }

        public string FindStrE(int x1, int y1, int x2, int y2, string str, string color, double sim)
        {
            return Marshal.PtrToStringUni(FindStrE(_dm, x1, y1, x2, y2, str, color, sim));

        }

        public string FindPicS(int x1, int y1, int x2, int y2, string pic_name, string delta_color, double sim, int dir, out int x, out int y)
        {
            object ox;
            object oy;
            string result = Marshal.PtrToStringUni(FindPicS(_dm, x1, y1, x2, y2, pic_name, delta_color, sim, dir, out ox, out oy));
            x = (int)ox;
            y = (int)oy;
            return result;

        }

        public string GetColorBGR(int x, int y)
        {
            return Marshal.PtrToStringUni(GetColorBGR(_dm, x, y));

        }

        public int WheelUp()
        {
            return WheelUp(_dm);

        }

        public string ReadDataAddr(int hwnd, int addr, int length)
        {
            return Marshal.PtrToStringUni(ReadDataAddr(_dm, hwnd, addr, length));

        }

        public string EnumIniSectionPwd(string file_name, string pwd)
        {
            return Marshal.PtrToStringUni(EnumIniSectionPwd(_dm, file_name, pwd));

        }

        public int SendCommand(string cmd)
        {
            return SendCommand(_dm, cmd);

        }

        public int KeyDown(int vk)
        {
            return KeyDown(_dm, vk);

        }

        public int EnableFakeActive(int en)
        {
            return EnableFakeActive(_dm, en);

        }

        public int MiddleDown()
        {
            return MiddleDown(_dm);

        }

        public int SetScreen(int width, int height, int depth)
        {
            return SetScreen(_dm, width, height, depth);

        }

        public string Ver()
        {
            return Marshal.PtrToStringUni(Ver(_dm));

        }

        public int SendStringIme(string str)
        {
            return SendStringIme(_dm, str);

        }

        public int Log(string info)
        {
            return Log(_dm, info);

        }

        public string Ocr(int x1, int y1, int x2, int y2, string color, double sim)
        {
            return Marshal.PtrToStringUni(Ocr(_dm, x1, y1, x2, y2, color, sim));

        }

        public int FindColor(int x1, int y1, int x2, int y2, string color, double sim, int dir, out int x, out int y)
        {
            object ox;
            object oy;
            int result = FindColor(_dm, x1, y1, x2, y2, color, sim, dir, out ox, out oy);
            x = (int)ox;
            y = (int)oy;
            return result;

        }

        public int UseDict(int index)
        {
            return UseDict(_dm, index);

        }

        public int MiddleUp()
        {
            return MiddleUp(_dm);

        }

        public string FindColorBlockEx(int x1, int y1, int x2, int y2, string color, double sim, int count, int width, int height)
        {
            return Marshal.PtrToStringUni(FindColorBlockEx(_dm, x1, y1, x2, y2, color, sim, count, width, height));

        }

        public int FoobarDrawLine(int hwnd, int x1, int y1, int x2, int y2, string color, int style, int width)
        {
            return FoobarDrawLine(_dm, hwnd, x1, y1, x2, y2, color, style, width);

        }

        public int SetMemoryFindResultToFile(string file_name)
        {
            return SetMemoryFindResultToFile(_dm, file_name);

        }

        public int EnableFontSmooth()
        {
            return EnableFontSmooth(_dm);

        }

        public string GetAveHSV(int x1, int y1, int x2, int y2)
        {
            return Marshal.PtrToStringUni(GetAveHSV(_dm, x1, y1, x2, y2));

        }

        public string FaqFetch()
        {
            return Marshal.PtrToStringUni(FaqFetch(_dm));

        }

        public int StrStr(string s, string str)
        {
            return StrStr(_dm, s, str);

        }

        public string GetWordsNoDict(int x1, int y1, int x2, int y2, string color)
        {
            return Marshal.PtrToStringUni(GetWordsNoDict(_dm, x1, y1, x2, y2, color));

        }

        public string StringToData(string string_value, int tpe)
        {
            return Marshal.PtrToStringUni(StringToData(_dm, string_value, tpe));

        }

        public int SetSimMode(int mode)
        {
            return SetSimMode(_dm, mode);

        }

        public int SetWindowText(int hwnd, string text)
        {
            return SetWindowText(_dm, hwnd, text);

        }

        public int DisableFontSmooth()
        {
            return DisableFontSmooth(_dm);

        }

        public int LeaveCri()
        {
            return LeaveCri(_dm);

        }

        public string GetCursorSpot()
        {
            return Marshal.PtrToStringUni(GetCursorSpot(_dm));

        }

        public int CmpColor(int x, int y, string color, double sim)
        {
            return CmpColor(_dm, x, y, color, sim);

        }

        public int UnBindWindow()
        {
            return UnBindWindow(_dm);

        }

        public int FindMultiColor(int x1, int y1, int x2, int y2, string first_color, string offset_color, double sim, int dir, out int x, out int y)
        {
            object ox;
            object oy;
            int result = FindMultiColor(_dm, x1, y1, x2, y2, first_color, offset_color, sim, dir, out ox, out oy);
            x = (int)ox;
            y = (int)oy;
            return result;

        }

        public int WriteInt(int hwnd, string addr, int tpe, int v)
        {
            return WriteInt(_dm, hwnd, addr, tpe, v);

        }

        public int SendPaste(int hwnd)
        {
            return SendPaste(_dm, hwnd);

        }

        public int SetWindowState(int hwnd, int flag)
        {
            return SetWindowState(_dm, hwnd, flag);

        }

        public string DoubleToData(double double_value)
        {
            return Marshal.PtrToStringUni(DoubleToData(_dm, double_value));

        }

        public int RunApp(string path, int mode)
        {
            return RunApp(_dm, path, mode);

        }

        public int SetPicPwd(string pwd)
        {
            return SetPicPwd(_dm, pwd);

        }

        public int GetNowDict()
        {
            return GetNowDict(_dm);

        }

        public string FindMultiColorE(int x1, int y1, int x2, int y2, string first_color, string offset_color, double sim, int dir)
        {
            return Marshal.PtrToStringUni(FindMultiColorE(_dm, x1, y1, x2, y2, first_color, offset_color, sim, dir));

        }

        public string EnumProcess(string name)
        {
            return Marshal.PtrToStringUni(EnumProcess(_dm, name));

        }

        public int LeftDoubleClick()
        {
            return LeftDoubleClick(_dm);

        }

        public int GetFileLength(string file_name)
        {
            return GetFileLength(_dm, file_name);

        }

        public int EnableKeypadMsg(int en)
        {
            return EnableKeypadMsg(_dm, en);

        }

        public int DisablePowerSave()
        {
            return DisablePowerSave(_dm);

        }

        public int CreateFoobarCustom(int hwnd, int x, int y, string pic, string trans_color, double sim)
        {
            return CreateFoobarCustom(_dm, hwnd, x, y, pic, trans_color, sim);

        }

        public int GetWordResultCount(string str)
        {
            return GetWordResultCount(_dm, str);

        }

        public int LockInput(int locks)
        {
            return LockInput(_dm, locks);

        }

        public int MoveTo(int x, int y)
        {
            return MoveTo(_dm, x, y);

        }

        public int EnableBind(int en)
        {
            return EnableBind(_dm, en);

        }

        public string FindMultiColorEx(int x1, int y1, int x2, int y2, string first_color, string offset_color, double sim, int dir)
        {
            return Marshal.PtrToStringUni(FindMultiColorEx(_dm, x1, y1, x2, y2, first_color, offset_color, sim, dir));

        }

        public int GetResultCount(string str)
        {
            return GetResultCount(_dm, str);

        }

        public string GetProcessInfo(int pid)
        {
            return Marshal.PtrToStringUni(GetProcessInfo(_dm, pid));

        }

        public string GetPath()
        {
            return Marshal.PtrToStringUni(GetPath(_dm));

        }

        public int LeftDown()
        {
            return LeftDown(_dm);

        }

        public int MoveFile(string src_file, string dst_file)
        {
            return MoveFile(_dm, src_file, dst_file);

        }

        public int SetWordGap(int word_gap)
        {
            return SetWordGap(_dm, word_gap);

        }

        public int DeleteFile(string file_name)
        {
            return DeleteFile(_dm, file_name);

        }

        public int BindWindow(int hwnd, string display, string mouse, string keypad, int mode)
        {
            return BindWindow(_dm, hwnd, display, mouse, keypad, mode);

        }

        public int EnableMouseAccuracy(int en)
        {
            return EnableMouseAccuracy(_dm, en);

        }

        public string FindDoubleEx(int hwnd, string addr_range, double double_value_min, double double_value_max, int steps, int multi_thread, int mode)
        {
            return Marshal.PtrToStringUni(FindDoubleEx(_dm, hwnd, addr_range, double_value_min, double_value_max, steps, multi_thread, mode));

        }

        public string GetCursorShape()
        {
            return Marshal.PtrToStringUni(GetCursorShape(_dm));

        }

        public string GetWordResultStr(string str, int index)
        {
            return Marshal.PtrToStringUni(GetWordResultStr(_dm, str, index));

        }

        public float ReadFloat(int hwnd, string addr)
        {
            return ReadFloat(_dm, hwnd, addr);

        }

        public int EnableDisplayDebug(int enable_debug)
        {
            return EnableDisplayDebug(_dm, enable_debug);

        }

        public int LockMouseRect(int x1, int y1, int x2, int y2)
        {
            return LockMouseRect(_dm, x1, y1, x2, y2);

        }

        public int WriteDouble(int hwnd, string addr, double v)
        {
            return WriteDouble(_dm, hwnd, addr, v);

        }

        public int FoobarFillRect(int hwnd, int x1, int y1, int x2, int y2, string color)
        {
            return FoobarFillRect(_dm, hwnd, x1, y1, x2, y2, color);

        }

        public int DecodeFile(string file_name, string pwd)
        {
            return DecodeFile(_dm, file_name, pwd);

        }

        public string FetchWord(int x1, int y1, int x2, int y2, string color, string word)
        {
            return Marshal.PtrToStringUni(FetchWord(_dm, x1, y1, x2, y2, color, word));

        }

        public string FindStringEx(int hwnd, string addr_range, string string_value, int tpe, int steps, int multi_thread, int mode)
        {
            return Marshal.PtrToStringUni(FindStringEx(_dm, hwnd, addr_range, string_value, tpe, steps, multi_thread, mode));

        }

        public int LoadPic(string pic_name)
        {
            return LoadPic(_dm, pic_name);

        }

        public int EncodeFile(string file_name, string pwd)
        {
            return EncodeFile(_dm, file_name, pwd);

        }

        public string ReadStringAddr(int hwnd, int addr, int tpe, int length)
        {
            return Marshal.PtrToStringUni(ReadStringAddr(_dm, hwnd, addr, tpe, length));

        }

        public int CheckInputMethod(int hwnd, string id)
        {
            return CheckInputMethod(_dm, hwnd, id);

        }

        public int IsFileExist(string file_name)
        {
            return IsFileExist(_dm, file_name);

        }

        public int SetDictPwd(string pwd)
        {
            return SetDictPwd(_dm, pwd);

        }

        public int FindWindowByProcess(string process_name, string class_name, string title_name)
        {
            return FindWindowByProcess(_dm, process_name, class_name, title_name);

        }

        public int GetMouseSpeed()
        {
            return GetMouseSpeed(_dm);

        }

        public int GetSpecialWindow(int flag)
        {
            return GetSpecialWindow(_dm, flag);

        }

        public int Is64Bit()
        {
            return Is64Bit(_dm);

        }

        public string GetDir(int tpe)
        {
            return Marshal.PtrToStringUni(GetDir(_dm, tpe));

        }

        public int SwitchBindWindow(int hwnd)
        {
            return SwitchBindWindow(_dm, hwnd);

        }

        public string FindStrWithFontE(int x1, int y1, int x2, int y2, string str, string color, double sim, string font_name, int font_size, int flag)
        {
            return Marshal.PtrToStringUni(FindStrWithFontE(_dm, x1, y1, x2, y2, str, color, sim, font_name, font_size, flag));

        }

        public int InitCri()
        {
            return InitCri(_dm);

        }

        public int Delays(int min_s, int max_s)
        {
            return Delays(_dm, min_s, max_s);

        }

        public int CaptureGif(int x1, int y1, int x2, int y2, string file_name, int delay, int time)
        {
            return CaptureGif(_dm, x1, y1, x2, y2, file_name, delay, time);

        }

        public string FindNearestPos(string all_pos, int tpe, int x, int y)
        {
            return Marshal.PtrToStringUni(FindNearestPos(_dm, all_pos, tpe, x, y));

        }

        public int AsmCall(int hwnd, int mode)
        {
            return AsmCall(_dm, hwnd, mode);

        }

        public int CreateFoobarEllipse(int hwnd, int x, int y, int w, int h)
        {
            return CreateFoobarEllipse(_dm, hwnd, x, y, w, h);

        }

        public int WriteIntAddr(int hwnd, int addr, int tpe, int v)
        {
            return WriteIntAddr(_dm, hwnd, addr, tpe, v);

        }

        public string GetDisplayInfo()
        {
            return Marshal.PtrToStringUni(GetDisplayInfo(_dm));

        }

        public string GetWords(int x1, int y1, int x2, int y2, string color, double sim)
        {
            return Marshal.PtrToStringUni(GetWords(_dm, x1, y1, x2, y2, color, sim));

        }

        public int SendString2(int hwnd, string str)
        {
            return SendString2(_dm, hwnd, str);

        }

        public int GetScreenWidth()
        {
            return GetScreenWidth(_dm);

        }

        public int FoobarUpdate(int hwnd)
        {
            return FoobarUpdate(_dm, hwnd);

        }

        public string GetEnv(int index, string name)
        {
            return Marshal.PtrToStringUni(GetEnv(_dm, index, name));

        }

        public int CheckUAC()
        {
            return CheckUAC(_dm);

        }

        public int SetDict(int index, string dict_name)
        {
            return SetDict(_dm, index, dict_name);

        }

        public int AddDict(int index, string dict_info)
        {
            return AddDict(_dm, index, dict_info);

        }

        public int DmGuard(int en, string tpe)
        {
            return DmGuard(_dm, en, tpe);

        }

        public int FindStrWithFont(int x1, int y1, int x2, int y2, string str, string color, double sim, string font_name, int font_size, int flag, out int x, out int y)
        {
            object ox;
            object oy;
            int result = FindStrWithFont(_dm, x1, y1, x2, y2, str, color, sim, font_name, font_size, flag, out ox, out oy);
            x = (int)ox;
            y = (int)oy;
            return result;

        }

        public double ReadDoubleAddr(int hwnd, int addr)
        {
            return ReadDoubleAddr(_dm, hwnd, addr);

        }

        public int FindInputMethod(string id)
        {
            return FindInputMethod(_dm, id);

        }

        public int DeleteIniPwd(string section, string key, string file_name, string pwd)
        {
            return DeleteIniPwd(_dm, section, key, file_name, pwd);

        }

        public int GetID()
        {
            return GetID(_dm);

        }

        public int SetUAC(int uac)
        {
            return SetUAC(_dm, uac);

        }

        public int OpenProcess(int pid)
        {
            return OpenProcess(_dm, pid);

        }

        public int SetPath(string path)
        {
            return SetPath(_dm, path);

        }

        public int GetForegroundFocus()
        {
            return GetForegroundFocus(_dm);

        }

        public int EnterCri()
        {
            return EnterCri(_dm);

        }

        public string FindShapeE(int x1, int y1, int x2, int y2, string offset_color, double sim, int dir)
        {
            return Marshal.PtrToStringUni(FindShapeE(_dm, x1, y1, x2, y2, offset_color, sim, dir));

        }

        public string FindPicMemEx(int x1, int y1, int x2, int y2, string pic_info, string delta_color, double sim, int dir)
        {
            return Marshal.PtrToStringUni(FindPicMemEx(_dm, x1, y1, x2, y2, pic_info, delta_color, sim, dir));

        }

        public int GetWordResultPos(string str, int index, out int x, out int y)
        {
            object ox;
            object oy;
            int result = GetWordResultPos(_dm, str, index, out ox, out oy);
            x = (int)ox;
            y = (int)oy;
            return result;

        }

        public int SetShowErrorMsg(int show)
        {
            return SetShowErrorMsg(_dm, show);

        }

        public int FindPic(int x1, int y1, int x2, int y2, string pic_name, string delta_color, double sim, int dir, out int x, out int y)
        {
            object ox;
            object oy;
            int result = FindPic(_dm, x1, y1, x2, y2, pic_name, delta_color, sim, dir, out ox, out oy);
            x = (int)ox;
            y = (int)oy;
            return result;

        }

        public int LeftClick()
        {
            return LeftClick(_dm);

        }

        public int WriteStringAddr(int hwnd, int addr, int tpe, string v)
        {
            return WriteStringAddr(_dm, hwnd, addr, tpe, v);

        }

        public int EnableKeypadPatch(int en)
        {
            return EnableKeypadPatch(_dm, en);

        }

        public int SetWindowSize(int hwnd, int width, int height)
        {
            return SetWindowSize(_dm, hwnd, width, height);

        }

        public string GetAveRGB(int x1, int y1, int x2, int y2)
        {
            return Marshal.PtrToStringUni(GetAveRGB(_dm, x1, y1, x2, y2));

        }

        public int GetWindowState(int hwnd, int flag)
        {
            return GetWindowState(_dm, hwnd, flag);

        }

        public int AsmClear()
        {
            return AsmClear(_dm);

        }

        public string IntToData(int int_value, int tpe)
        {
            return Marshal.PtrToStringUni(IntToData(_dm, int_value, tpe));

        }

        public int FindShape(int x1, int y1, int x2, int y2, string offset_color, double sim, int dir, out int x, out int y)
        {
            object ox;
            object oy;
            int result = FindShape(_dm, x1, y1, x2, y2, offset_color, sim, dir, out ox, out oy);
            x = (int)ox;
            y = (int)oy;
            return result;

        }

        public int DownloadFile(string url, string save_file, int timeout)
        {
            return DownloadFile(_dm, url, save_file, timeout);

        }

        public int Reg(string code, string Ver)
        {
            return Reg(_dm, code, Ver);

        }

        public int SetMinColGap(int col_gap)
        {
            return SetMinColGap(_dm, col_gap);

        }

        public int EnableRealMouse(int en, int mousedelay, int mousestep)
        {
            return EnableRealMouse(_dm, en, mousedelay, mousestep);

        }

        public string BGR2RGB(string bgr_color)
        {
            return Marshal.PtrToStringUni(BGR2RGB(_dm, bgr_color));

        }

        public int FoobarSetSave(int hwnd, string file_name, int en, string header)
        {
            return FoobarSetSave(_dm, hwnd, file_name, en, header);

        }

        public int FoobarClearText(int hwnd)
        {
            return FoobarClearText(_dm, hwnd);

        }

        public int delay(int mis)
        {
            return delay(_dm, mis);

        }

        public string SelectFile()
        {
            return Marshal.PtrToStringUni(SelectFile(_dm));

        }

        public int KeyPress(int vk)
        {
            return KeyPress(_dm, vk);

        }

        public int SetEnumWindowDelay(int delay)
        {
            return SetEnumWindowDelay(_dm, delay);

        }

        public int Play(string file_name)
        {
            return Play(_dm, file_name);

        }

        public string ReadData(int hwnd, string addr, int length)
        {
            return Marshal.PtrToStringUni(ReadData(_dm, hwnd, addr, length));

        }

        public int SetKeypadDelay(string tpe, int delay)
        {
            return SetKeypadDelay(_dm, tpe, delay);

        }

        public int FindWindowEx(int parent, string class_name, string title_name)
        {
            return FindWindowEx(_dm, parent, class_name, title_name);

        }

        public int SetMouseSpeed(int speed)
        {
            return SetMouseSpeed(_dm, speed);

        }

        public int FreeProcessMemory(int hwnd)
        {
            return FreeProcessMemory(_dm, hwnd);

        }

        public int FoobarSetFont(int hwnd, string font_name, int size, int flag)
        {
            return FoobarSetFont(_dm, hwnd, font_name, size, flag);

        }

        public int FoobarDrawPic(int hwnd, int x, int y, string pic, string trans_color)
        {
            return FoobarDrawPic(_dm, hwnd, x, y, pic, trans_color);

        }

        public int Capture(int x1, int y1, int x2, int y2, string file_name)
        {
            return Capture(_dm, x1, y1, x2, y2, file_name);

        }

        public string GetWindowProcessPath(int hwnd)
        {
            return Marshal.PtrToStringUni(GetWindowProcessPath(_dm, hwnd));

        }

        public int GetCursorPos(out int x, out int y)
        {
            object ox;
            object oy;
            int result = GetCursorPos(_dm, out ox, out oy);
            x = (int)ox;
            y = (int)oy;
            return result;

        }

        public int FindPicMem(int x1, int y1, int x2, int y2, string pic_info, string delta_color, double sim, int dir, out int x, out int y)
        {
            object ox;
            object oy;
            int result = FindPicMem(_dm, x1, y1, x2, y2, pic_info, delta_color, sim, dir, out ox, out oy);
            x = (int)ox;
            y = (int)oy;
            return result;

        }

        public int TerminateProcess(int pid)
        {
            return TerminateProcess(_dm, pid);

        }

        public int Beep(int fre, int delay)
        {
            return Beep(_dm, fre, delay);

        }

        public string EnumWindow(int parent, string title, string class_name, int filter)
        {
            return Marshal.PtrToStringUni(EnumWindow(_dm, parent, title, class_name, filter));

        }

        public string FindFloatEx(int hwnd, string addr_range, float float_value_min, float float_value_max, int steps, int multi_thread, int mode)
        {
            return Marshal.PtrToStringUni(FindFloatEx(_dm, hwnd, addr_range, float_value_min, float_value_max, steps, multi_thread, mode));

        }

        public string OcrExOne(int x1, int y1, int x2, int y2, string color, double sim)
        {
            return Marshal.PtrToStringUni(OcrExOne(_dm, x1, y1, x2, y2, color, sim));

        }

        public int RegEx(string code, string Ver, string ip)
        {
            return RegEx(_dm, code, Ver, ip);

        }

        public string GetCommandLine(int hwnd)
        {
            return Marshal.PtrToStringUni(GetCommandLine(_dm, hwnd));

        }

        public string FindShapeEx(int x1, int y1, int x2, int y2, string offset_color, double sim, int dir)
        {
            return Marshal.PtrToStringUni(FindShapeEx(_dm, x1, y1, x2, y2, offset_color, sim, dir));

        }

        public float ReadFloatAddr(int hwnd, int addr)
        {
            return ReadFloatAddr(_dm, hwnd, addr);

        }

        public string GetWindowTitle(int hwnd)
        {
            return Marshal.PtrToStringUni(GetWindowTitle(_dm, hwnd));

        }

        public string EnumWindowByProcess(string process_name, string title, string class_name, int filter)
        {
            return Marshal.PtrToStringUni(EnumWindowByProcess(_dm, process_name, title, class_name, filter));

        }

        public int FoobarTextRect(int hwnd, int x, int y, int w, int h)
        {
            return FoobarTextRect(_dm, hwnd, x, y, w, h);

        }

        public string GetWindowClass(int hwnd)
        {
            return Marshal.PtrToStringUni(GetWindowClass(_dm, hwnd));

        }

        public string RGB2BGR(string rgb_color)
        {
            return Marshal.PtrToStringUni(RGB2BGR(_dm, rgb_color));

        }

        public int FindWindowByProcessId(int process_id, string class_name, string title_name)
        {
            return FindWindowByProcessId(_dm, process_id, class_name, title_name);

        }

        public string GetNetTime()
        {
            return Marshal.PtrToStringUni(GetNetTime(_dm));

        }

        public int KeyPressStr(string key_str, int delay)
        {
            return KeyPressStr(_dm, key_str, delay);

        }

        public int GetColorNum(int x1, int y1, int x2, int y2, string color, double sim)
        {
            return GetColorNum(_dm, x1, y1, x2, y2, color, sim);

        }

        public string AsmCode(int base_addr)
        {
            return Marshal.PtrToStringUni(AsmCode(_dm, base_addr));

        }

        public int WriteData(int hwnd, string addr, string data)
        {
            return WriteData(_dm, hwnd, addr, data);

        }

        public int FoobarDrawText(int hwnd, int x, int y, int w, int h, string text, string color, int align)
        {
            return FoobarDrawText(_dm, hwnd, x, y, w, h, text, color, align);

        }

        public int IsDisplayDead(int x1, int y1, int x2, int y2, int t)
        {
            return IsDisplayDead(_dm, x1, y1, x2, y2, t);

        }

        public int EnableGetColorByCapture(int en)
        {
            return EnableGetColorByCapture(_dm, en);

        }

        public int GetWindowRect(int hwnd, out int x1, out int y1, out int x2, out int y2)
        {
            object ox1;
            object oy1;
            object ox2;
            object oy2;
            int result = GetWindowRect(_dm, hwnd, out ox1, out oy1, out ox2, out oy2);
            x1 = (int)ox1;
            y1 = (int)oy1;
            x2 = (int)ox2;
            y2 = (int)oy2;
            return result;

        }

        public string Assemble(string asm_code, int base_addr, int is_upper)
        {
            return Marshal.PtrToStringUni(Assemble(_dm, asm_code, base_addr, is_upper));

        }

        public int FreePic(string pic_name)
        {
            return FreePic(_dm, pic_name);

        }

        public string GetPicSize(string pic_name)
        {
            return Marshal.PtrToStringUni(GetPicSize(_dm, pic_name));

        }

        public int FindColorBlock(int x1, int y1, int x2, int y2, string color, double sim, int count, int width, int height, out int x, out int y)
        {
            object ox;
            object oy;
            int result = FindColorBlock(_dm, x1, y1, x2, y2, color, sim, count, width, height, out ox, out oy);
            x = (int)ox;
            y = (int)oy;
            return result;

        }

        public int FreeScreenData(int handle)
        {
            return FreeScreenData(_dm, handle);

        }

        public int CaptureJpg(int x1, int y1, int x2, int y2, string file_name, int quality)
        {
            return CaptureJpg(_dm, x1, y1, x2, y2, file_name, quality);

        }

        public int WriteDataAddr(int hwnd, int addr, string data)
        {
            return WriteDataAddr(_dm, hwnd, addr, data);

        }

        public int SendStringIme2(int hwnd, string str, int mode)
        {
            return SendStringIme2(_dm, hwnd, str, mode);

        }

        public int SetEnv(int index, string name, string value)
        {
            return SetEnv(_dm, index, name, value);

        }

        public string SelectDirectory()
        {
            return Marshal.PtrToStringUni(SelectDirectory(_dm));

        }

        public string FindStrS(int x1, int y1, int x2, int y2, string str, string color, double sim, out int x, out int y)
        {
            object ox;
            object oy;
            string result = Marshal.PtrToStringUni(FindStrS(_dm, x1, y1, x2, y2, str, color, sim, out ox, out oy));
            x = (int)ox;
            y = (int)oy;
            return result;

        }

        public string FindIntEx(int hwnd, string addr_range, int int_value_min, int int_value_max, int tpe, int steps, int multi_thread, int mode)
        {
            return Marshal.PtrToStringUni(FindIntEx(_dm, hwnd, addr_range, int_value_min, int_value_max, tpe, steps, multi_thread, mode));

        }

        public int EnableMouseMsg(int en)
        {
            return EnableMouseMsg(_dm, en);

        }

        public string AppendPicAddr(string pic_info, int addr, int size)
        {
            return Marshal.PtrToStringUni(AppendPicAddr(_dm, pic_info, addr, size));

        }

        public int CreateFoobarRoundRect(int hwnd, int x, int y, int w, int h, int rw, int rh)
        {
            return CreateFoobarRoundRect(_dm, hwnd, x, y, w, h, rw, rh);

        }

        public int GetForegroundWindow()
        {
            return GetForegroundWindow(_dm);

        }

        public int LockDisplay(int locks)
        {
            return LockDisplay(_dm, locks);

        }

        public int SetDisplayAcceler(int level)
        {
            return SetDisplayAcceler(_dm, level);

        }

        public string ExcludePos(string all_pos, int tpe, int x1, int y1, int x2, int y2)
        {
            return Marshal.PtrToStringUni(ExcludePos(_dm, all_pos, tpe, x1, y1, x2, y2));

        }

        public int WriteFile(string file_name, string content)
        {
            return WriteFile(_dm, file_name, content);

        }

        public string GetBasePath()
        {
            return Marshal.PtrToStringUni(GetBasePath(_dm));

        }

        public string GetNetTimeByIp(string ip)
        {
            return Marshal.PtrToStringUni(GetNetTimeByIp(_dm, ip));

        }

        public int FoobarStopGif(int hwnd, int x, int y, string pic_name)
        {
            return FoobarStopGif(_dm, hwnd, x, y, pic_name);

        }

        public string ReadFile(string file_name)
        {
            return Marshal.PtrToStringUni(ReadFile(_dm, file_name));

        }

        public int EnableIme(int en)
        {
            return EnableIme(_dm, en);

        }

        public int RegNoMac(string code, string Ver)
        {
            return RegNoMac(_dm, code, Ver);

        }

        public string FindStrFastS(int x1, int y1, int x2, int y2, string str, string color, double sim, out int x, out int y)
        {
            object ox;
            object oy;
            string result = Marshal.PtrToStringUni(FindStrFastS(_dm, x1, y1, x2, y2, str, color, sim, out ox, out oy));
            x = (int)ox;
            y = (int)oy;
            return result;

        }

        public int FoobarUnlock(int hwnd)
        {
            return FoobarUnlock(_dm, hwnd);

        }

        public int SetMouseDelay(string tpe, int delay)
        {
            return SetMouseDelay(_dm, tpe, delay);

        }

        public int ImageToBmp(string pic_name, string bmp_name)
        {
            return ImageToBmp(_dm, pic_name, bmp_name);

        }

        public int GetScreenData(int x1, int y1, int x2, int y2)
        {
            return GetScreenData(_dm, x1, y1, x2, y2);

        }

        public int GetMousePointWindow()
        {
            return GetMousePointWindow(_dm);

        }

        public int ReadIntAddr(int hwnd, int addr, int tpe)
        {
            return ReadIntAddr(_dm, hwnd, addr, tpe);

        }

        public int WriteIni(string section, string key, string v, string file_name)
        {
            return WriteIni(_dm, section, key, v, file_name);

        }

        public int SetClipboard(string data)
        {
            return SetClipboard(_dm, data);

        }

        public string FindDataEx(int hwnd, string addr_range, string data, int steps, int multi_thread, int mode)
        {
            return Marshal.PtrToStringUni(FindDataEx(_dm, hwnd, addr_range, data, steps, multi_thread, mode));

        }

        public string GetNetTimeSafe()
        {
            return Marshal.PtrToStringUni(GetNetTimeSafe(_dm));

        }

        public string FindPicMemE(int x1, int y1, int x2, int y2, string pic_info, string delta_color, double sim, int dir)
        {
            return Marshal.PtrToStringUni(FindPicMemE(_dm, x1, y1, x2, y2, pic_info, delta_color, sim, dir));

        }

        public string FloatToData(float float_value)
        {
            return Marshal.PtrToStringUni(FloatToData(_dm, float_value));

        }

        public int FoobarSetTrans(int hwnd, int trans, string color, double sim)
        {
            return FoobarSetTrans(_dm, hwnd, trans, color, sim);

        }

        public int CapturePre(string file_name)
        {
            return CapturePre(_dm, file_name);

        }

        public string EnumIniKey(string section, string file_name)
        {
            return Marshal.PtrToStringUni(EnumIniKey(_dm, section, file_name));

        }

        public int KeyUpChar(string key_str)
        {
            return KeyUpChar(_dm, key_str);

        }

        public int WriteDoubleAddr(int hwnd, int addr, double v)
        {
            return WriteDoubleAddr(_dm, hwnd, addr, v);

        }

        public int GetClientRect(int hwnd, out int x1, out int y1, out int x2, out int y2)
        {
            object ox1;
            object oy1;
            object ox2;
            object oy2;
            int result = GetClientRect(_dm, hwnd, out ox1, out oy1, out ox2, out oy2);
            x1 = (int)ox1;
            y1 = (int)oy1;
            x2 = (int)ox2;
            y2 = (int)oy2;
            return result;

        }

        public int MiddleClick()
        {
            return MiddleClick(_dm);

        }

        public int GetScreenDepth()
        {
            return GetScreenDepth(_dm);

        }

        public string FindPicE(int x1, int y1, int x2, int y2, string pic_name, string delta_color, double sim, int dir)
        {
            return Marshal.PtrToStringUni(FindPicE(_dm, x1, y1, x2, y2, pic_name, delta_color, sim, dir));

        }

        public int FaqIsPosted()
        {
            return FaqIsPosted(_dm);

        }

        public string ReadString(int hwnd, string addr, int tpe, int length)
        {
            return Marshal.PtrToStringUni(ReadString(_dm, hwnd, addr, tpe, length));

        }

        public string GetMac()
        {
            return Marshal.PtrToStringUni(GetMac(_dm));

        }

        public int GetBindWindow()
        {
            return GetBindWindow(_dm);

        }

        public int RightUp()
        {
            return RightUp(_dm);

        }

        public string FindFloat(int hwnd, string addr_range, float float_value_min, float float_value_max)
        {
            return Marshal.PtrToStringUni(FindFloat(_dm, hwnd, addr_range, float_value_min, float_value_max));

        }

        public string ReadIni(string section, string key, string file_name)
        {
            return Marshal.PtrToStringUni(ReadIni(_dm, section, key, file_name));

        }

        public string EnumIniSection(string file_name)
        {
            return Marshal.PtrToStringUni(EnumIniSection(_dm, file_name));

        }

        public int FoobarTextLineGap(int hwnd, int gap)
        {
            return FoobarTextLineGap(_dm, hwnd, gap);

        }

        public int GetDmCount()
        {
            return GetDmCount(_dm);

        }

        public int IsBind(int hwnd)
        {
            return IsBind(_dm, hwnd);

        }

        public string FaqSend(string server, int handle, int request_type, int time_out)
        {
            return Marshal.PtrToStringUni(FaqSend(_dm, server, handle, request_type, time_out));

        }

        public int RightClick()
        {
            return RightClick(_dm);

        }









        #region 继承释放接口方法
        public void Dispose()
        {
            //必须为true
            Dispose(true);
            //通知垃圾回收机制不再调用终结器（析构器）
            GC.SuppressFinalize(this);
        }

        public void Close()
        {
            Dispose();
        }

        ~CDmSoft()
        {
            //必须为false
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                // 清理托管资源
                //if (managedResource != null)
                //{
                //    managedResource.Dispose();
                //    managedResource = null;
                //}
            }
            // 清理非托管资源
            if (_dm != IntPtr.Zero)
            {
                UnBindWindow();
                _dm = IntPtr.Zero;
                int ret = FreeDM();
            }
            //让类型知道自己已经被释放
            disposed = true;
        }
        #endregion
    }
}
