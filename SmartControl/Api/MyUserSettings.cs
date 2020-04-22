using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Policy;
using System.Text;
using System.Drawing;

namespace SmartControl.Api
{
    internal class MyUserSettings : ApplicationSettingsBase
    {
        public void Restore(DataManager data)
        {
            data.Url = Url;
            data.Port = Port;
            data.Credentials.UserName = UserName;
            data.Credentials.Password = Password;
        }

        public void Save(DataManager data)
        {
            Url = data.Url;
            Port = data.Port;
            UserName = data.Credentials.UserName;
            Password = data.Credentials.Password;
        }


        [UserScopedSetting()]
        [DefaultSettingValue("http://localhost:22654/")]
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
