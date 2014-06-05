using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using TaskScheduler;

namespace TestWPF
{
    class Class1
    {
        public void foo()
        {
            TaskSchedulerClass ts = new TaskSchedulerClass();
            ts.Connect(null, null, null, null);
            IRunningTaskCollection tasks = ts.GetRunningTasks(1);
            foreach (IRunningTask task in tasks)
            {
                Console.WriteLine(task.Name);
            }
        }

        public XPathNodeIterator filenames(string stockrefofbrand, string color, string colordescription, string text, string con)
        {
            var photos = text.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            var res = new List<string>(photos.Length);

            if (photos.Length > 0)
            {
                foreach (var name in photos)
                {
                    if (!importedPhotosRegistry.Contains(pkfileobjectsid(stockrefofbrand, name) + colordescription))
                    {
                        res.Add(name);
                    }
                }
                fileCleenup(stockrefofbrand, color, colordescription, photos, con);
            }
            return sys_iterate(res.ToArray());
        }

        private bool _cleanup = false;
        private void fileCleenup(string stockrefofbrand, string color, string colordescription, string[] photos, string con)
        {
            if (_cleanup || con == null) return;
            _cleanup = true;
            var basequery = @"(select o.id
            from str_fileobjects o
            inner join str_product r on r.id = o.productid and r.variantvalue2 = o.variantvalue2
            where r.manufacturercode = '" + stockrefofbrand + @"' 
                and r.variantvalue2 ilike '" + colordescription + " " + color + @"'
                and o.filename not in (" + string.Join(",", photos.Select(i => "'" + i + "'")) + "))";

            DbUtils.Query("delete from sys_scripts_importedidentifiers where tablename='str_fileobjects' and systemid in " + basequery, con);
            DbUtils.Query("delete from str_fileobjects where id in " + basequery, con);
        }

        private XPathNodeIterator sys_iterate(string[] p)
        {
            throw new NotImplementedException();
        }

        class DbUtils
        {

            internal static void Query(string p, string con)
            {
                throw new NotImplementedException();
            }
        }

        List<string> importedPhotosRegistry;
        private string pkfileobjectsid(string stockrefofbrand, string name)
        {
            throw new NotImplementedException();
        }
    }
}
