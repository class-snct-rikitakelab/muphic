﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.1
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReleaseTool.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("muphic ver.7b\\muphic\\bin\\x86\\Release\\muphic.exe")]
        public string MuphicReleasePath {
            get {
                return ((string)(this["MuphicReleasePath"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("muphic ver.7b\\muphic\\bin\\x86\\Debug\\muphic.exe")]
        public string MuphicDebugPath {
            get {
                return ((string)(this["MuphicDebugPath"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("muphic ver.7b\\muphic\\bin\\x86\\Archive\\data01.dat")]
        public string MuphicArchivePath {
            get {
                return ((string)(this["MuphicArchivePath"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("muphic ver.7b\\ConfigurationTool\\bin\\x86\\Release\\muphic 動作設定ツール.exe")]
        public string MuphicConfPath {
            get {
                return ((string)(this["MuphicConfPath"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("%str1%ファイルが見つかりません。")]
        public string FileNotfoundTitle {
            get {
                return ((string)(this["FileNotfoundTitle"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("パス : %str1%")]
        public string FileNotfoundMessage {
            get {
                return ((string)(this["FileNotfoundMessage"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("muphic ver.%str1%")]
        public string DistDirectoryName {
            get {
                return ((string)(this["DistDirectoryName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("発行フォルダ %str1% は既に存在します。上書きしますか？")]
        public string OverwriteMessage {
            get {
                return ((string)(this["OverwriteMessage"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("上書き確認")]
        public string OverwriteTitle {
            get {
                return ((string)(this["OverwriteTitle"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("muphic ver.7b\\muphic\\bin\\x86\\Release\\JpnKanaConversion.dll")]
        public string JpnKanaConversionDllReleasePath {
            get {
                return ((string)(this["JpnKanaConversionDllReleasePath"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("muphic ver.7b\\muphic\\bin\\x86\\Release\\JpnKanaConvHelper.dll")]
        public string JpnKanaConvHelperDllReleasePath {
            get {
                return ((string)(this["JpnKanaConvHelperDllReleasePath"]));
            }
        }
    }
}
