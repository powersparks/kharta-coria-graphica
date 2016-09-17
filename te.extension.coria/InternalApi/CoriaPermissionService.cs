using System;
using Telligent.Evolution.Extensibility.Security.Version1;
using Telligent.Evolution.Extensibility.Version1;
using TEApi = Telligent.Evolution.Extensibility.Api.Version1.PublicApi;

namespace te.extension.coria.InternalApi
{
    internal class CoriaPermissionService
    {
        internal static bool CanCreateMapBook(Guid applicationTypeId, Guid applicationId)
        {
            if (applicationTypeId == Plugins.Application.CoriaMapBookType._applicationTypeId)
                return CheckPermission(applicationId, CoriaPermissions.CreateMapId);
            else if (applicationTypeId == TEApi.Groups.ContentTypeId)
                return TEApi.NodePermissions.Get("groups", applicationId, CoriaPermissions.CreateMapId.ToString()).IsAllowed;
             
            return false;
        }
        internal static bool CheckPermission(Guid id, Guid permissionId)
        {
            var mapbook = InternalApi.CoriaDataService.GetCoriaMapBookApplication(id);

            if (mapbook == null)
                return false;

            return TEApi.NodePermissions.Get("MapBook", mapbook.ApplicationId, permissionId.ToString()).IsAllowed;
        }
    }
    public class CoriaPermissions : IPermissionRegistrar, ITranslatablePlugin, ICategorizedPlugin
    {
        private static readonly Guid _createMapId = new Guid("aada35bb-fde2-4ddd-876e-a998647e98e7");
        private static readonly Guid _readMapId = new Guid("9beb28ce-0177-4749-b269-5b981fa034ed");
        private static readonly Guid _updateMapId = new Guid("5fa86c53-6e27-494e-9b4e-6a84005d8af6");
        private static readonly Guid _deleteMapId = new Guid("6927c1c0-2357-44b7-8df5-db0cb2141f86");


        public static Guid CreateMapId { get { return _createMapId; } }
        public static Guid ReadMapId { get { return _readMapId; } }
        public static Guid UpdateMapId { get { return _updateMapId; } }
        public static Guid DeleteMapId { get { return _deleteMapId; } }



        private ITranslatablePluginController _translation;

        #region IPlugin Members

        public string Name
        {
            get { return "Coria Mapping: Map Permissions"; }
        }

        public string Description
        {
            get { return "Registers permissions for Coria \"Map Books\" and \"Mapping\" application."; }
        }

        public void Initialize()
        {

        }

        #endregion

        #region IPermissionRegistrar Members

        public void RegisterPermissions(IPermissionRegistrarController permissionController)
        {
            permissionController.Register(
               new Permission(
                 CreateMapId,
                 "Permission_MapBook_CreateMap_Name",
                 "Permission_MapBook_CreateMap_Description",
                 _translation,
                 TEApi.Groups.ApplicationTypeId,
                 new PermissionConfiguration()
                 {
                     Joinless = new JoinlessGroupPermissionConfiguration { Administrators = true, Moderators = false, RegisteredUsers = false, Owners = true, Everyone = false },
                     PublicOpen = new MembershipGroupPermissionConfiguration { Owners = true, Managers = false, Members = false, Everyone = false },
                     PublicClosed = new MembershipGroupPermissionConfiguration { Owners = true, Managers = false, Members = false, Everyone = false },
                     PrivateListed = new MembershipGroupPermissionConfiguration { Owners = true, Managers = false, Members = false },
                     PrivateUnlisted = new MembershipGroupPermissionConfiguration { Owners = true, Managers = false, Members = false }
                 }
               )
             );
            permissionController.Register(
               new Permission(
                 ReadMapId,
                 "Permission_MapBook_ReadMap_Name",
                 "Permission_MapBook_ReadMap_Description",
                 _translation,
                 TEApi.Groups.ApplicationTypeId,
                 new PermissionConfiguration()
                 {
                     Joinless = new JoinlessGroupPermissionConfiguration { Administrators = true, Moderators = true, RegisteredUsers = true, Owners = true, Everyone = true },
                     PublicOpen = new MembershipGroupPermissionConfiguration { Owners = true, Managers = true, Members = true, Everyone = true },
                     PublicClosed = new MembershipGroupPermissionConfiguration { Owners = true, Managers = true, Members = true, Everyone = true },
                     PrivateListed = new MembershipGroupPermissionConfiguration { Owners = true, Managers = true, Members = true },
                     PrivateUnlisted = new MembershipGroupPermissionConfiguration { Owners = true, Managers = true, Members = true }
                 }
               )
             );
            permissionController.Register(
               new Permission(
                 UpdateMapId,
                 "Permission_MapBook_UpdateMap_Name",
                 "Permission_MapBook_UpdateMap_Description",
                 _translation,
                 TEApi.Groups.ApplicationTypeId,
                 new PermissionConfiguration()
                 {
                     Joinless = new JoinlessGroupPermissionConfiguration { Administrators = true, Moderators = false, RegisteredUsers = false, Owners = true, Everyone = false },
                     PublicOpen = new MembershipGroupPermissionConfiguration { Owners = true, Managers = false, Members = false, Everyone = false },
                     PublicClosed = new MembershipGroupPermissionConfiguration { Owners = true, Managers = false, Members = false, Everyone = false },
                     PrivateListed = new MembershipGroupPermissionConfiguration { Owners = true, Managers = false, Members = false },
                     PrivateUnlisted = new MembershipGroupPermissionConfiguration { Owners = true, Managers = false, Members = false }
                 }
               )
             );
            permissionController.Register(
               new Permission(
                 DeleteMapId,
                 "Permission_MapBook_DeleteMap_Name",
                 "Permission_MapBook_DeleteMap_Description",
                 _translation,
                 TEApi.Groups.ApplicationTypeId,
                 new PermissionConfiguration()
                 {
                     Joinless = new JoinlessGroupPermissionConfiguration { Administrators = true, Moderators = false, RegisteredUsers = false, Owners = true, Everyone = false },
                     PublicOpen = new MembershipGroupPermissionConfiguration { Owners = true, Managers = true, Members = false, Everyone = false },
                     PublicClosed = new MembershipGroupPermissionConfiguration { Owners = true, Managers = true, Members = true, Everyone = false },
                     PrivateListed = new MembershipGroupPermissionConfiguration { Owners = true, Managers = true, Members = true },
                     PrivateUnlisted = new MembershipGroupPermissionConfiguration { Owners = true, Managers = true, Members = true }
                 }
               )
             );

        }

        #endregion

        #region ITranslatablePlugin Members
        private Translation[] _defaultTranslations;
        public Translation[] DefaultTranslations
        {
            get
            {
                if (_defaultTranslations != null)
                    return _defaultTranslations;
                var translation = new Translation("en-US");
                translation.Set("Permission_MapBook_CreateMap_Name", "MapBook - Create MapBook");
                translation.Set("Permission_MapBook_CreateMap_Description", "This permission determines who can create maps");
                translation.Set("Permission_MapBook_ReadMap_Name", "MapBook - View MapBook");
                translation.Set("Permission_MapBook_ReadMap_Description", "This permission determines who can view maps");
                translation.Set("Permission_MapBook_UpdateMap_Name", "MapBook - Update MapBook");
                translation.Set("Permission_MapBook_UpdateMap_Description", "This permission determines who can update maps");
                translation.Set("Permission_MapBook_DeleteMap_Name", "MapBook - Delete MapBook");
                translation.Set("Permission_MapBook_DeleteMap_Description", "This permission determines who can delete maps");

                _defaultTranslations = new[] { translation };

                return _defaultTranslations;
            }
        }

        public void SetController(ITranslatablePluginController controller)
        {
            _translation = controller;
        }

        #endregion

        #region ICategorizedPlugin Members

        public string[] Categories { get { return new[] { "Translatable" }; } }

        #endregion
    }

}
