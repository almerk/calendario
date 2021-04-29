using NUnit.Framework;
using Calendario.Core;
namespace Calendario.UnitTests.Core
{
    public class AccessPermissionsTests
    {

        [Test]
        public void ConstructorWithReadPermission_CanReadReturnsTrue()
        {
            var accessRules = new AccessPermissions(Permissions.Read);
            Assert.True(accessRules.CanRead);
        }

        [Test]
        public void ConstructorWithNoReadPermission_CanReadReturnsFalse()
        {
            var accessRules = new AccessPermissions();
            Assert.False(accessRules.CanRead);
        }

        [Test]
        public void ConstructorWithUpdatePermission_CanUpdateReturnsTrue()
        {
            var accessRules = new AccessPermissions(Permissions.Update);
            Assert.True(accessRules.CanUpdate);
        }

        [Test]
        public void ConstructorWithNoUpdatePermission_CanUpdateReturnsFalse()
        {
            var accessRules = new AccessPermissions(Permissions.Read);
            Assert.False(accessRules.CanUpdate);
        }

        [Test]
        public void ConstructorWithDeletePermission_CanDeleteReturnsTrue()
        {
            var accessRules = new AccessPermissions(Permissions.Delete);
            Assert.True(accessRules.CanDelete);
        }

        [Test]
        public void ConstructorWithCreatePermission_CanCreateReturnsTrue()
        {
            var accessRules = new AccessPermissions(Permissions.CreateDependents);
            Assert.True(accessRules.CanCreateDependents);
        }

        [Test]
        public void ConstructorWithBothCreatePermissionAndCanRead_BothReturnTrue()
        {
            var accessRules = new AccessPermissions(Permissions.CreateDependents | Permissions.Read);
            Assert.True(accessRules.CanCreateDependents && accessRules.CanRead);
        }



    }
}