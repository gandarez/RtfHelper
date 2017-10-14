﻿using System.IO;
using System.Text;
using Kbg.NppPluginNET.PluginInfrastructure;
using System;
using System.Windows.Forms;
using RtfHelper.Formatter;

namespace Kbg.NppPluginNET
{
    class Main
    {
        public static string Name = "RtfHelper";

        private static RtfFormatter rtfFormatter = new RtfFormatter();

        //   static bool someSetting = false;
        //   static frmMyDlg frmMyDlg = null;
        //   static int idMyDlg = -1;
        //   static Bitmap tbBmp = Properties.Resources.star;
        //   static Bitmap tbBmp_tbTab = Properties.Resources.star_bmp;
        //   static Icon tbIcon = null;

        public static void OnNotification(ScNotification notification)
        {

            // This method is invoked whenever something is happening in notepad++
            // use eg. as
            // if (notification.Header.Code == (uint)NppMsg.NPPN_xxx)
            // { ... }
            // or
            //
            // if (notification.Header.Code == (uint)SciMsg.SCNxxx)
            // { ... }
        }

        internal static void CommandMenuInit()
        {
            string iniFilePath = null;

            StringBuilder sbIniFilePath = new StringBuilder(Win32.MAX_PATH);
            Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_GETPLUGINSCONFIGDIR, Win32.MAX_PATH, sbIniFilePath);
            iniFilePath = sbIniFilePath.ToString();
            if (!Directory.Exists(iniFilePath)) Directory.CreateDirectory(iniFilePath);
            iniFilePath = Path.Combine(iniFilePath, Name + ".ini");
            //   someSetting = (Win32.GetPrivateProfileInt("SomeSection", "SomeKey", 0, iniFilePath) != 0);

            SetCommands();
        }

        private static void SetCommands()
        {
            PluginBase.SetCommand(0, "MyMenuCommand", myMenuFunction, new ShortcutKey(false, false, false, Keys.None));
            // PluginBase.SetCommand(1, "MyDockableDialog", myDockableDialog); idMyDlg = 1;
        }

        internal static void SetToolBarIcon()
        {
            //toolbarIcons tbIcons = new toolbarIcons();
            //tbIcons.hToolbarBmp = tbBmp.GetHbitmap();
            //IntPtr pTbIcons = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons));
            //Marshal.StructureToPtr(tbIcons, pTbIcons, false);
            //Win32.SendMessage(PluginBase.nppData._nppHandle, (uint)NppMsg.NPPM_ADDTOOLBARICON, PluginBase._funcItems.Items[idMyDlg]._cmdID, pTbIcons);
            //Marshal.FreeHGlobal(pTbIcons);
        }

        internal static void PluginCleanUp()
        {
           // Win32.WritePrivateProfileString("SomeSection", "SomeKey", someSetting ? "1" : "0", iniFilePath);
        }

        private static void myMenuFunction()
        {
            IntPtr currentScint = PluginBase.GetCurrentScintilla();
            ScintillaGateway scintillaGateway = new ScintillaGateway(currentScint);
            int length = scintillaGateway.GetLength();
            string allText = scintillaGateway.GetText(length + 1);

            string newText = rtfFormatter.GetFormattedText(allText);

            scintillaGateway.SelectAll();
            scintillaGateway.ReplaceSel(newText.ToString());
        }

        //internal static void myDockableDialog()
        //{
        //    if (frmMyDlg == null)
        //    {
        //        frmMyDlg = new frmMyDlg();

        //        using (Bitmap newBmp = new Bitmap(16, 16))
        //        {
        //            Graphics g = Graphics.FromImage(newBmp);
        //            ColorMap[] colorMap = new ColorMap[1];
        //            colorMap[0] = new ColorMap();
        //            colorMap[0].OldColor = Color.Fuchsia;
        //            colorMap[0].NewColor = Color.FromKnownColor(KnownColor.ButtonFace);
        //            ImageAttributes attr = new ImageAttributes();
        //            attr.SetRemapTable(colorMap);
        //            g.DrawImage(tbBmp_tbTab, new Rectangle(0, 0, 16, 16), 0, 0, 16, 16, GraphicsUnit.Pixel, attr);
        //            tbIcon = Icon.FromHandle(newBmp.GetHicon());
        //        }

        //        NppTbData _nppTbData = new NppTbData();
        //        _nppTbData.hClient = frmMyDlg.Handle;
        //        _nppTbData.pszName = "My dockable dialog";
        //        _nppTbData.dlgID = idMyDlg;
        //        _nppTbData.uMask = NppTbMsg.DWS_DF_CONT_RIGHT | NppTbMsg.DWS_ICONTAB | NppTbMsg.DWS_ICONBAR;
        //        _nppTbData.hIconTab = (uint)tbIcon.Handle;
        //        _nppTbData.pszModuleName = PluginName;
        //        IntPtr _ptrNppTbData = Marshal.AllocHGlobal(Marshal.SizeOf(_nppTbData));
        //        Marshal.StructureToPtr(_nppTbData, _ptrNppTbData, false);

        //        Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_DMMREGASDCKDLG, 0, _ptrNppTbData);
        //    }
        //    else
        //    {
        //        Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_DMMSHOW, 0, frmMyDlg.Handle);
        //    }
        //}
    }
}