using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te.extension.coria.InternalApi
{
    internal class CoriaDataService
    {

        internal static InternalApi.Map GetMapApplication(Guid applicationId)
        {
            throw new NotImplementedException();
        }
        //private static readonly List<CoriaApplication> AllApplications = new List<CoriaApplication>()
        //{
        //    new CoriaApplication() { ApplicationId = new Guid("cd3cdc13-8b4f-4dfd-9a97-9e5c06d1fa73"), Name = "Map", Description = "Displays a Map from a source" },
        //    new CoriaApplication() { ApplicationId = new Guid("15238a71-e923-463c-9df8-a98f022b0760"), Name = "List", Description = "Displays a List from a source" },
        //    new CoriaApplication() { ApplicationId = new Guid("71333f10-468c-4a5f-9a30-a8bb30dec3d9"), Name = "Chart", Description = "Displays a Chart from a source." }
        //};

        //private static readonly List<Map> AllLinks = new List<Map>()
        //{
        //    new Map() { ApplicationId = new Guid("cd3cdc13-8b4f-4dfd-9a97-9e5c06d1fa73"), ContentId = new Guid("f01c4ef7-25e0-4b61-8722-891fc8049202"), Name = "Google", Description = "Search the world's information, including webpages, images, videos and more.", Url = "http://www.google.com" },
        //    new Map() { ApplicationId = new Guid("cd3cdc13-8b4f-4dfd-9a97-9e5c06d1fa73"), ContentId = new Guid("df022553-3b96-409b-9f8a-0cdecfa4f35d"), Name = "Bing", Description = "Bing helps you turn information into action, making it faster and easier to go from searching to doing.", Url = "http://www.bing.com" },
        //    new Map() { ApplicationId = new Guid("cd3cdc13-8b4f-4dfd-9a97-9e5c06d1fa73"), ContentId = new Guid("fb70bffe-9707-4fbd-bfd1-9238385afb14"), Name = "Yahoo", Description = "The new Yahoo experience makes it easier to discover the news and information that you care about most.", Url = "http://www.yahoo.com" },

        //    new List() { ApplicationId = new Guid("15238a71-e923-463c-9df8-a98f022b0760"), ContentId = new Guid("713933ad-2651-41a0-9eba-cf27061ae52c"), Name = "Telligent", Description = "Telligent is an enterprise collaboration and community software company", Url = "http://www.telligent.com" },
        //    new List() { ApplicationId = new Guid("15238a71-e923-463c-9df8-a98f022b0760"), ContentId = new Guid("cd3f9006-0d1d-4558-b543-b0ddac1ac2f8"), Name = "Telligent Community", Description = "", Url = "http://community.telligent.com" },
        //    new List() { ApplicationId = new Guid("15238a71-e923-463c-9df8-a98f022b0760"), ContentId = new Guid("4b0ea76d-9e14-4ee7-a7ca-304901e1f90d"), Name = "Verint", Description = "Verint solutions capture, distill, and analyze complex and underused information sources (such as voice, video, and unstructured text) to help organizations make timely and effective decisions.", Url = "http://www.verint.com" },

        //    new LinkItem() { ApplicationId = new Guid("71333f10-468c-4a5f-9a30-a8bb30dec3d9"), ContentId = new Guid("0b3a382e-b407-4606-8182-0462beeb0252"), Name = "Jquery", Description = "The Write Less, Do More, JavaScript Library.", Url = "http://www.jquery.com" },
        //    new LinkItem() { ApplicationId = new Guid("71333f10-468c-4a5f-9a30-a8bb30dec3d9"), ContentId = new Guid("8d9e4444-19ad-42ba-938a-723645e3979d"), Name = "Asp.Net", Description = "Home of the Microsoft ASP.NET development community", Url = "http://www.asp.net" },
        //    new LinkItem() { ApplicationId = new Guid("71333f10-468c-4a5f-9a30-a8bb30dec3d9"), ContentId = new Guid("a7936a19-aa21-438b-b1d7-45dd0eb9b525"), Name = "GitHub", Description = "Where people build software", Url = "https://github.com" },
        //    new LinkItem() { ApplicationId = new Guid("71333f10-468c-4a5f-9a30-a8bb30dec3d9"), ContentId = new Guid("f3a82dcf-4517-43d2-91b4-ede4c35db3e1"), Name = "Stack Overflow", Description = "A language-independent collaboratively edited question and answer site for programmers.", Url = "http://stackoverflow.com/" },
        //    new LinkItem() { ApplicationId = new Guid("71333f10-468c-4a5f-9a30-a8bb30dec3d9"), ContentId = new Guid("437f8f2c-45bb-4408-9486-8392e8ddcdd8"), Name = "Apache Tomcat", Description = "An open source software implementation of the Java Servlet, JavaServer Pages, Java Expression Language and Java WebSocket technologies. ", Url = "http://tomcat.apache.org/" },
        //    new LinkItem() { ApplicationId = new Guid("71333f10-468c-4a5f-9a30-a8bb30dec3d9"), ContentId = new Guid("2ecacd3d-6345-485a-88e7-e6e2827433eb"), Name = "Less Css", Description = "Less extends CSS with dynamic behavior such as variables, mixins, operations and functions.", Url = "http://lesscss.org" },
        //    new LinkItem() { ApplicationId = new Guid("71333f10-468c-4a5f-9a30-a8bb30dec3d9"), ContentId = new Guid("83873c74-afd5-4385-a877-bf6c613ef4c2"), Name = "Apache Solr", Description = "An open-source search server based on the Lucene Java search library.", Url = "http://lucene.apache.org/solr/" },
        //};

        //public static LinksApplication GetApplication(Guid applicationId)
        //{
        //    return AllApplications.FirstOrDefault(a => a.ApplicationId == applicationId);
        //}

        //public static List<LinksApplication> ListApplications()
        //{
        //    return AllApplications;
        //}

        //public static LinkItem GetLink(Guid contentId)
        //{
        //    return AllLinks.FirstOrDefault(a => a.ContentId == contentId);
        //}

        //public static List<LinkItem> ListLinks(Guid applicationId)
        //{
        //    return AllLinks.Where(a => a.ApplicationId == applicationId).ToList();
        //}
    }
}
