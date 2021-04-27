using System;
namespace Calendario.Core
{
    public record AccessPermissions
    {
        public AccessPermissions(){}
        public AccessPermissions(Permissions permissions)
        {
            Predicate<Permissions> have  = 
                (p) => Convert.ToBoolean(permissions & p);

            CanRead = have(Permissions.Read);
            CanUpdate = have(Permissions.Update);
            CanDelete = have(Permissions.Delete);
            CanCreateDependents = have(Permissions.CreateDependents);
        }
        
        public bool CanRead { get; init; }
        public bool CanUpdate { get; init; }
        public bool CanDelete { get; init; }
        public bool CanCreateDependents { get; init; }
    }

    [Flags]
    public enum Permissions:int
    {
        Read = 1,
        Update = 2,
        Delete = 4,
        CreateDependents = 8 
    } 
}