using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Policy;
using System.Text;
using System.Drawing;
using Api.Data;

namespace SmartControl.Settings
{
    internal class MyUserSettings : ApplicationSettingsBase
    {
        public void Restore(ConnectSettings data)
        {
            data.Url = Url;
            data.Port = Port;
        }

        public void Restore(Credentials data)
        {
            data.UserName = UserName;
            data.Password = Password;
        }

        public void Save(ConnectSettings data)
        {
            Url = data.Url;
            Port = data.Port;
        }

        public void Save(Credentials data)
        {
            UserName = data.UserName;
            Password = data.Password;
        }


        [UserScopedSetting()]
        [DefaultSettingValue("http://localhost")]
        public string Url
        {
            get
            {
                return this[nameof(Url)] as string;
            }
            set
            {
                this[nameof(Url)] = (string)value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("22654")]
        public int Port
        {
            get
            {
                return Int32.Parse(this[nameof(Port)] as string ?? "22654");
            }
            set
            {
                this[nameof(Port)] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("User")]
        public string UserName
        {
            get
            {
                return this[nameof(UserName)] as string;
            }
            set
            {
                this[nameof(UserName)] = (string)value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public string Password
        {
            get
            {
                return this[nameof(Password)] as string;
            }
            set
            {
                this[nameof(Password)] = (string)value;
            }
        }

        /// <summary>
        /// Singleton implementation
        /// </summary>
        private static MyUserSettings m_oInstance = null;
        public static MyUserSettings Instance
        {
            get
            {
                if (m_oInstance == null)
                {
                    m_oInstance = new MyUserSettings();
                }
                return m_oInstance;
            }
        }
        private MyUserSettings() { }
    }
}
