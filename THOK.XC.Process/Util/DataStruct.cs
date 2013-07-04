using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.AS.Stocking.Util
{
    /// <summary>
    /// 节目区域结构
    /// </summary>
    public struct User_PartInfo
    {
        public int iX;             // 窗口的起点X
        public int iY;             // 窗口的起点Y
        public int iWidth;         // 窗体的宽度
        public int iHeight;        // 窗体的高度
        public int iFrameMode;     // 边框的样式
        public int FrameColor;     // 边框颜色
    }

    /// <summary>
    /// 字体设置
    /// </summary>
    public struct User_FontSet
    {
        public string strFontName;          // 字体的名称
        public int iFontSize;              // 字体的大小
        public int bFontBold;              // 字体是否加粗
        public int bFontItaic;             // 字体是否是斜体
        public int bFontUnderline;         // 字体是否带下划线
        public int colorFont;              // 字体的颜色
        public int iAlignStyle;            // 左右对齐方式 : 0-左对齐 ,1-左右居中 ,2-右对齐

        public int iVAlignerStyle;         // 上下对齐方式 : 0-顶对齐 ,1-上下居中 ,2-底对齐
        public int iRowSpace;              // 行间距
    }

    /// <summary>
    /// 动作方式设置
    /// </summary>
    public struct User_MoveSet
    {
        public int iActionType;           // 节目变换方式
        public int iActionSpeed;          // 节目的播放速度,取值0～30
        public int bClear;                // 是否需要清除背景
        public int iHoldTime;             // 在屏幕上停留的时间,单位0.1秒

        public int iClearSpeed;           // 清除显示屏的速度
        public int iClearActionType;      // 节目清除的变换方式
        public int iFrameTime;            // 每帧时间
    }


    /// <summary>
    /// 图文框
    /// </summary>
    public struct User_Bmp
    {
        public User_PartInfo PartInfo;     // 分区信息
    }

    /// <summary>
    /// 单行文本框
    /// </summary>
    public struct User_SingleText
    {
        public string chContent;           // 显示内容
        public User_PartInfo PartInfo;     // 分区信息
        public int BkColor;               // 背景颜色
        public User_FontSet FontInfo;      // 字体设置
        public User_MoveSet MoveSet;       // 动作方式设置
    }

    /// <summary>
    /// 文本框
    /// </summary>
    public struct User_Text
    {
        public string chContent;           // 显示内容
        public User_PartInfo PartInfo;     // 分区信息
        public int BkColor;               // 背景颜色
        public User_FontSet FontInfo;      // 字体设置
        public User_MoveSet MoveSet;       // 动作方式设置
    }

    /// <summary>
    /// RTF文件
    /// </summary>
    public struct User_RTF
    {
        public string strFileName;         // RTF文件名
        public User_PartInfo PartInfo;     // 分区信息
        public User_MoveSet MoveSet;       // 动作方式设置
    }

    /// <summary>
    /// 计时窗口
    /// </summary>
    public struct User_Timer
    {
        public User_PartInfo PartInfo;     // 分区信息
        public int BkColor;               // 背景颜色
        public User_FontSet FontInfo;      // 字体设置

        public int ReachTimeYear;         // 到达年
        public int ReachTimeMonth;        // 到达月
        public int ReachTimeDay;          // 到达日
        public int ReachTimeHour;         // 到达时
        public int ReachTimeMinute;       // 到达分
        public int ReachTimeSecond;       // 到达秒

        public int bDay;                  // 是否显示天 0－不显示 1－显示
        public int bHour;                 // 是否显示小时
        public int bMin;                  // 是否显示分钟
        public int bSec;                  // 是否显示秒
        public int bMulOrSingleLine;      // 单行还是多行
        public string chTitle;             // 添加显示文字
    }


    /// <summary>
    /// 温度窗口
    /// </summary>
    public struct User_Temperature
    {
        public User_PartInfo PartInfo;     // 分区信息
        public int BkColor;               // 背景颜色
        public User_FontSet FontInfo;      // 字体设置
        public string chTitle;             // 标题
        public int iDisplayType;          // 显示格式：0－度 1－C
    }

    /// <summary>
    /// 日期时间窗口
    /// </summary>
    public struct User_DateTime
    {
        public User_PartInfo PartInfo;     // 分区信息
        public int BkColor;               // 背景颜色
        public User_FontSet FontInfo;      // 字体设置
        public int iDisplayType;          // 显示风格
        public string chTitle;             // 添加显示文字
        public int bYearDisType;          // 年份位数0 －4；1－2位
        public int bMulorSingleLine;      // 单行还是多行

        public int bYear;                 // 是否显示年，0－不显示，1－显示
        public int bMouth;                // 是否显示月 
        public int bDay;                  // 是否显示天  
        public int bWeek;                 // 是否显示星期  
        public int bHour;                 // 是否显示小时   
        public int bMin;                  // 是否显示分钟
        public int bSec;                  // 是否显示秒
    }
    
}
