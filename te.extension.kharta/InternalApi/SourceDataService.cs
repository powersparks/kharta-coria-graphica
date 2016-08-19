using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kharta.coria.graphica.Models;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;
using System.Text.RegularExpressions;
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

        internal static KhartaSource GetSourceApplication(Guid id)
        {

            Source _source = new Source();
            KhartaSource _khSource = new KhartaSource();
            Func<Source, KhartaSource> toKhartaSource = (Source fromSource) => ToKhartaSource(fromSource);

            using (KhartaDataModel dbcontext = new KhartaDataModel())
            {
                _source = (from s in dbcontext.Sources
                           where s.ApplicationId.Equals(id)
                           select s).FirstOrDefault();
                if (_source != null)
                {
                    _khSource = ToKhartaSource(_source);
                }
            }
            return _khSource;
        }

        internal static IList<Source> GetGroupSourceApplications(int groupId) {
            IList<Source> sources = new List<Source>();
            using (KhartaDataModel dbcontext = new KhartaDataModel())
            {
                sources = (from s in dbcontext.Sources
                            where s.GroupId.Equals(groupId)
                            select s).ToList();
            }
                //IList<Source> sources = new List<Source>();
            return sources;
             
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
                        var exception = ex;

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
        #region SqlInstallMethods
        internal static void Install(string fileName)
        {
            using (KhartaDataModel dbcontext =new KhartaDataModel())
            { 
                foreach (string statement in GetStatementsFromSqlBatch(Utility.EmbeddedResources.GetString("te.extension.kharta.Resources.Sql." + fileName)))
                {
                    dbcontext.Database.ExecuteSqlCommand(statement);
                     
                }
                dbcontext.SaveChanges();
            }
        }
        private static IEnumerable<string> GetStatementsFromSqlBatch(string sqlBatch)
        {
            // This isn't as reliable as the SQL Server SDK, but works for most SQL batches and prevents another assembly reference
            foreach (string statement in Regex.Split(sqlBatch, @"^\s*GO\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline))
            {
                string sanitizedStatement = Regex.Replace(statement, @"(?:^SET\s+.*?$|\/\*.*?\*\/|--.*?$)", "\r\n", RegexOptions.IgnoreCase | RegexOptions.Multiline).Trim();
                if (sanitizedStatement.Length > 0)
                    yield return sanitizedStatement;
            }
        }
        #endregion



    }
}
