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
        }

        public void Save(DataManager data)
        {
            Url = data.Url;
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
