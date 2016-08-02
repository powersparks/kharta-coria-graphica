using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Content.Version1;
using Telligent.Evolution.Extensibility.Version1;
using Telligent.Evolution.Extensibility.Api.Version1;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;
 
namespace te.extension.kharta.Plugins.Content
{
    public class SourceContentType : IPlugin, IContentType, ITranslatablePlugin
    /* TODO: IPermissions , ITaggableContentType, ISearchableContentType,  
     *  IScriptedContentFragmentExtension, 
     *  IGroupNewPostLinkPlugin
     *  
     */
    {
        ITranslatablePluginController _translation;
        IContentStateChanges _contentState;
        //IHashTagController _hashTagController;
        //IMentionController _mentionController;

        /// <summary>
        /// Application Types, should be a guid array of Kharta Applications.
        /// For now, it's the Groups application type id
        /// </summary>
        public Guid[] ApplicationTypes { get { return new Guid[] { TEApi.Groups.ApplicationTypeId }; } }

        public Guid ContentTypeId { get { return PublicApi.Sources.ContentTypeId; } }

        public string ContentTypeName { get { return "KhartaSourceContent"; } }

        public string Description { get { return "Kharta Source Content"; } }

        public string Name { get { return "Kharta Source"; } }

        public void AttachChangeEvents(IContentStateChanges stateChanges) { _contentState = stateChanges; }

        public IContent Get(Guid contentId) { return null; }//return PublicApi.Sources.Get(contentId); }

        public void Initialize() { }


        #region ITranslatablePlugin Members

        public Translation[] DefaultTranslations
        {
            get
            {
                var translation = new Translation("en-US");
                translation.Set("content_type_name", "Source Content");
                return new Translation[] { translation };
            }
        }

        public void SetController(ITranslatablePluginController controller) { _translation = controller; }

        #endregion
    }
}