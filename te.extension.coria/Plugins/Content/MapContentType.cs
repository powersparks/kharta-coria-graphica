using System;
using Telligent.Evolution.Extensibility.Content.Version1;
using Telligent.Evolution.Extensibility.Version1;

namespace te.extension.coria.Plugins.Content
{
    public class MapContentType : IPlugin, IContentType
    /* TODO: , ITranslatablePlugin, IPermissions , ITaggableContentType, ISearchableContentType,  
     *  IScriptedContentFragmentExtension, 
     *  IGroupNewPostLinkPlugin
     *  
     */
    {
       // ITranslatablePluginController _translation;
        IContentStateChanges _contentState;
        //IHashTagController _hashTagController;
        //IMentionController _mentionController;

        /// <summary>
        /// Application Types, should be a guid array of Coria Applications.
        /// For now, it's the Groups application type id
        /// </summary>
        public Guid[] ApplicationTypes { get { return new Guid[] { Application.CoriaMapType._applicationTypeId };  } }

        public Guid ContentTypeId { get { return  PublicApi.Maps.ContentTypeId; } }

        public string ContentTypeName { get { return "CoriaMapContent"; } }

        public string Description { get { return "Coria Map Content"; } }

        public string Name { get { return "Coria Map"; } }

        public void AttachChangeEvents(IContentStateChanges stateChanges) { _contentState = stateChanges; }

        public IContent Get(Guid contentId) { PublicApi.Map map = new PublicApi.Map(); return map; }// PublicApi.Maps.Get(contentId); }

        public void Initialize() {   }


    }
}
