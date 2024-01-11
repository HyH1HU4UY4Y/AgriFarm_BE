using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SharedPermissionAuth.Commons
{
    public record AgriPermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsRoot = false)
    {
        public string Name => NameFor(Action, Resource);
        public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
    }

    public static class ResourceScope
    {
        public const string Users = nameof(Users);
        public const string Values = nameof(Values);

    }

    public static class ResourceAction
    {
        public const string View = nameof(View);
        public const string Search = nameof(Search);
        public const string Create = nameof(Create);
        public const string Update = nameof(Update);
        public const string Delete = nameof(Delete);
        public const string Export = nameof(Export);
        public const string UpgradeSubscription = nameof(UpgradeSubscription);
    }

    public static class AppPermissions
    {
        private static readonly AgriPermission[] _all = new AgriPermission[]
        {
            new("View Users", ResourceAction.View, ResourceScope.Users),
            new("Create Users", ResourceAction.Create, ResourceScope.Users),
            new("Update Users", ResourceAction.Update, ResourceScope.Users),
            new("Delete Users", ResourceAction.Delete, ResourceScope.Users),

            new("View Values", ResourceAction.View, ResourceScope.Values),
            new("Create Values", ResourceAction.Create, ResourceScope.Values),
            new("Update Values", ResourceAction.Update, ResourceScope.Values),
            new("Delete Values", ResourceAction.Delete, ResourceScope.Values),



        };

        public static IReadOnlyList<AgriPermission> All { get; } = new ReadOnlyCollection<AgriPermission>(_all);
        public static IReadOnlyList<AgriPermission> Root { get; } = new ReadOnlyCollection<AgriPermission>(_all.Where(p => p.IsRoot).ToArray());
        public static IReadOnlyList<AgriPermission> Admin { get; } = new ReadOnlyCollection<AgriPermission>(_all.Where(p => !p.IsRoot).ToArray());
        public static IReadOnlyList<AgriPermission> Basic { get; } = new ReadOnlyCollection<AgriPermission>(_all.Where(p => p.IsBasic).ToArray());
    }
}
