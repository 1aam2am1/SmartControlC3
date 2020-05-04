using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SmartControl.Api.Extensions
{
    public static class Extensions
    {
        public static List<T> Resize<T>(this List<T> list, int sz) where T : new()
        {
            int cur = list.Count;
            if (sz < cur)
                list.RemoveRange(sz, cur - sz);
            else if (sz > cur)
            {
                if (sz > list.Capacity)//this bit is purely an optimization, to avoid multiple automatic capacity changes.
                    list.Capacity = sz;
                for (; cur < sz; ++cur)
                {
                    list.Add(new T());
                }

            }
            return list;
        }

        public static ObservableCollection<T> Populate<T>(this ObservableCollection<T> ob, int sz) where T : new()
        {
            for (int cur = ob.Count; cur < sz; ++cur)
            {
                ob.Add(new T());
            }
            for (int i = sz; i < ob.Count;)
            {
                ob.RemoveAt(ob.Count - 1);
            }

            return ob;
        }

        public static ObservableCollection<T> Copy<T>(this ObservableCollection<T> ob, List<T> sz)
        {
            for (int i = 0; i < sz.Count; ++i)
            {
                if (i < ob.Count)
                {
                    ob[i] = sz[i];
                }
                else
                {
                    ob.Add(sz[i]);
                }
            }
            for (int i = sz.Count; i < ob.Count;)
            {
                ob.RemoveAt(ob.Count - 1);
            }


            return ob;
        }
    }
}
