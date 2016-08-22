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

using Telligent.Evolution.Extensibility.Version1;
 

namespace kharta.coria.graphica.core
{
    public class GraphicaCore : IPlugin, IInstallablePlugin
    {
        public string Name { get { return "Graphica Applications Core"; } }
        public string Description { get { return "Provides the Graphica Data Model and installs and upgrades a Kharta database using EF"; } }
         
        private static readonly Version _emptyVersion = new Version(0, 0, 0, 0);
        public Version Version { get { return GetType().Assembly.GetName().Version; } }
        public void Initialize() {         } 
        public void Install(Version lastInstalledVersion)
        {
            #region Install SQL
            if (lastInstalledVersion == null || lastInstalledVersion.Major == 0)
            {
               // InstallSql("HostingCreateTable.sql");
           
            }
            if (lastInstalledVersion == null || lastInstalledVersion <= new Version(1, 1))
            {

            }
          
           #endregion
        }
        public void Uninstall()
        {
            //throw new NotImplementedException();
        }
        #region SqlInstallMethods
        internal static void InstallSql(string fileName)
        {
            using (KhartaDataModel dbcontext = new KhartaDataModel())
            {
                foreach (string statement in GetStatementsFromSqlBatch(Utility.EmbeddedResources.GetString("kharta.coria.graphica.Resources.Sql." + fileName)))
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
