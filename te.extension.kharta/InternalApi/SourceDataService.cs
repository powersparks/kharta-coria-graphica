using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kharta.coria.graphica.Models;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;

using System.Reflection;

[assembly: InternalsVisibleTo("kharta.coria.graphica.test")]
namespace te.extension.kharta.InternalApi
{
    internal class SourceDataService
    {
        internal static KhartaSource CreateNewSourceApplication(KhartaSource khartaSource) {
            
            Source _source = new Source();
            Func<KhartaSource, Source> toSource = (KhartaSource fromKhartaSource) => ToSource(fromKhartaSource);   
            Func<Source, KhartaSource> toKhartaSource = (Source fromSource) => ToKhartaSource(fromSource);
            _source = ToSource(khartaSource);
            using (KhartaDataModel dbcontext = new KhartaDataModel()) {
                 
                 _source = dbcontext.Sources.Add(_source);
                dbcontext.SaveChanges();
                khartaSource = ToKhartaSource(_source);
            } 
            return khartaSource;
        }
        internal static KhartaSource GetSourceApplication(int id)
        {

            Source _source = new Source();
            KhartaSource _khSource = new KhartaSource();
            Func<Source, KhartaSource> toKhartaSource = (Source fromSource) => ToKhartaSource(fromSource);

            using (KhartaDataModel dbcontext = new KhartaDataModel())
            {
                _source = (from s in dbcontext.Sources
                           where s.Id.Equals(id)
                           select s).FirstOrDefault();
                if (_source != null)
                {
                    _khSource = ToKhartaSource(_source);
                }
            }
            return _khSource;
        }
        internal static KhartaSource AddUpdateSourceApplication(KhartaSource khartaSource)
        {
            if (khartaSource.Id == 0)
            {
                khartaSource = CreateNewSourceApplication(khartaSource);
            }
            else  {
                Func<KhartaSource, Source> toSource = (KhartaSource fromKhartaSource) => ToSource(fromKhartaSource);
                Func<Source, KhartaSource> toKhartaSource = (Source fromSource) => ToKhartaSource(fromSource);
                Source _updateSource = new Source();
                _updateSource =  toSource(khartaSource);
                using (KhartaDataModel dbcontext = new KhartaDataModel())
                {
                    try
                    {
                        dbcontext.Entry(_updateSource).State = System.Data.Entity.EntityState.Modified;

                        dbcontext.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException ex) {
                        //ex contains the message related to the entity was delete or updated external
                        // unit test deletes the record
                        //TODO: handle the condition   

                    }
                    
                }
                khartaSource = GetSourceApplication(_updateSource.Id);  
            }
             
            return khartaSource;
        }
        private static Source ToSource(KhartaSource fromKhartaSource)
        {
            Type ks = fromKhartaSource.GetType();
            Source source = new Source();
            Type s = source.GetType();
            IList<PropertyInfo> ksproperties = new List<PropertyInfo>(ks.GetProperties());
            IList<PropertyInfo> properties = new List<PropertyInfo>(s.GetProperties());
            foreach (PropertyInfo prop in properties)
            {
                PropertyInfo sProp = ks.GetProperty(prop.Name);
                int sourcePropertyIndex = ksproperties.IndexOf(sProp);
                if (sourcePropertyIndex > -1)
                {
                    sProp = properties[sourcePropertyIndex];

                    if (sProp.CanRead)
                    {
                        var value = sProp.GetValue(fromKhartaSource);
                        if (prop.CanWrite)
                            prop.SetValue(source, value);
                    }

                }

            }
            return source;
        }
        private static KhartaSource ToKhartaSource(Source fromSource)
        {
            KhartaSource khartaSource = new KhartaSource();
            Type ks = khartaSource.GetType();
            
            Type s = fromSource.GetType();
            IList<PropertyInfo> ksproperties = new List<PropertyInfo>(ks.GetProperties());
            IList<PropertyInfo> properties = new List<PropertyInfo>(s.GetProperties());
            foreach (PropertyInfo prop in ksproperties)
            {
                if (prop.CanWrite)
                {
                    PropertyInfo sProp = s.GetProperty(prop.Name);
                    int sourcePropertyIndex = properties.IndexOf(sProp);
                    
                    if (sourcePropertyIndex > -1)
                    {
                        
                         sProp = properties[sourcePropertyIndex];
                        if (sProp.CanRead)
                        {
                            var value = sProp.GetValue(fromSource);
                            prop.SetValue(khartaSource, value);
                        }
                    }
                }

            }
            return khartaSource;
        }
        internal static Object CreateNewSourceApplicationObject(KhartaSource khartaSourc) {
            
            object Kharta =  CreateNewSourceApplication(khartaSourc) ;
            return Kharta;
        }
        internal static void DeleteSourceApplication(KhartaSource khartaSource) {
            using (KhartaDataModel dbcontext = new KhartaDataModel())
            { 
                    var result = (from s in dbcontext.Sources
                                  where s.Id.Equals(khartaSource.Id)
                                  select s).FirstOrDefault();
                Source _source = result;
                dbcontext.Sources.Remove(_source);
                    dbcontext.SaveChanges();
                 
            }

        }

       
    }
}
