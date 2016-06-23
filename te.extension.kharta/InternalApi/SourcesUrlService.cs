using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Common.QueryModel;
using Telligent.Evolution.Extensibility.Caching.Version1;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;

namespace te.extension.kharta.InternalApi
{
    internal class SourcesUrlService
    {

        internal static string GetUrl(int sourceId)
        {
            var source = InternalApi.SourceDataService.GetSourceApplication(sourceId);
            if (source == null)
                return null;

            var group = TEApi.Groups.Get(source.ApplicationId);
            if (group == null)
                return null;

            return TEApi.Url.BuildUrl("sources_view", group.Id.Value, new Dictionary<string, string>() { { "SourceId", source.Id.ToString() } });
        }

    }
}
