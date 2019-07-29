using Core.Entities;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Migrations
{
    public static class InitialMethods
    {
        // ProductCategories
        private static readonly Guid _categoryGardenId = Guid.NewGuid();
        private static readonly Guid _categoryDiyId = Guid.NewGuid();
        private static readonly Guid _categoryHouseholdId = Guid.NewGuid();

        // Users
        private static readonly Guid _user1Id = Guid.NewGuid();
        private static readonly Guid _user2Id = Guid.NewGuid();
        private static readonly Guid _user3Id = Guid.NewGuid();

        // Products
        private static readonly Guid _product1Id = Guid.NewGuid();
        private static readonly Guid _product2Id = Guid.NewGuid();
        private static readonly Guid _product3Id = Guid.NewGuid();
        private static readonly Guid _product4Id = Guid.NewGuid();
        private static readonly Guid _product5Id = Guid.NewGuid();
        private static readonly Guid _product6Id = Guid.NewGuid();
        private static readonly Guid _product7Id = Guid.NewGuid();
        private static readonly Guid _product8Id = Guid.NewGuid();
        private static readonly Guid _product9Id = Guid.NewGuid();
        private static readonly Guid _product10Id = Guid.NewGuid();

        // Orders
        private static readonly Guid _order1Id = Guid.NewGuid();
        private static readonly Guid _order2Id = Guid.NewGuid();
        private static readonly Guid _order3Id = Guid.NewGuid();
        private static readonly Guid _order4Id = Guid.NewGuid();
        private static readonly Guid _order5Id = Guid.NewGuid();

        public static void ProductCategory(MigrationBuilder migrationBuilder) => migrationBuilder.InsertData(
            table: "Productcategories",
            columns: new[] { "Id", "Name", "DisplayName" },
            values: new object[,]
            {
                {_categoryGardenId, "garden", "Garden"},
                {_categoryDiyId, "diy", "DIY Tool"},
                {_categoryHouseholdId, "household", "Household"}
            });

        public static void User(MigrationBuilder migrationBuilder) => migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "Id", "UserName", "DisplayName"},
            values: new object[,]
            {
                {_user1Id, "jemmahahn@gmail.com", "Jemma Hahn"},
                {_user2Id, "millerhead@gmail.com", "Miller Head"},
                {_user3Id, "keatonli@gmail.com", "Keaton Li"}
            });

        public static void Product(MigrationBuilder migrationBuilder) => migrationBuilder.InsertData(
            table: "Products",
            columns: new[] { "Id", "Code", "DisplayName", "Description", "ProductCategoryId" },
            values: new object[,]
            {
                {_product1Id, "ho12345678901", "bottle opener", "sörnyitó", _categoryHouseholdId},
                {_product2Id, "ga12345678901", "bucket", "vödör", _categoryGardenId },
                {_product3Id, "di12345678901", "drill machine", "fúrógép", _categoryDiyId},
                {_product4Id, "di12345678902", "buzz saw", "körfűrész", _categoryDiyId},
                {_product5Id, "ho12345678902", "rolling_pin", "sodrófa, nyújtófa", _categoryHouseholdId},
                {_product6Id, "ho12345678903", "can opener", "konzervnyitó", _categoryHouseholdId},
                {_product7Id, "ga12345678902", "garden hose", "kerti tömlő", _categoryGardenId},
                {_product8Id, "di12345678903", "hammer ", "kalapács", _categoryDiyId},
                {_product9Id, "ho12345678904", "needle ", "tű", _categoryHouseholdId},
                {_product10Id, "ga12345678903", "rake ", "gereblye", _categoryGardenId},
            });

        public static void Order(MigrationBuilder migrationBuilder) => migrationBuilder.InsertData(
            table: "Orders",
            columns: new[] { "Id", "UserId", "OrderDate"},
            values: new object[,]
            {
                { _order1Id, _user1Id, new DateTime(2019, 07, 22, 10,05,44,0)},

                { _order2Id, _user1Id, new DateTime(2019, 07, 23, 9,35,4,0)},

                { _order3Id, _user3Id, new DateTime(2019, 06, 2, 10,25,20,0)},

                { _order4Id, _user1Id, new DateTime(2019, 07, 25, 15,44,33,0)},

                { _order5Id, _user3Id, new DateTime(2019, 05, 29, 16,52,3,0)},
            });

        public static void OrderProduct(MigrationBuilder migrationBuilder) => migrationBuilder.InsertData(
            table: "OrderProducts",
            columns: new[] { "OrderId", "ProductId", "Amount" },
            values: new object[,] {
                { _order1Id, _product1Id, 1 }, 
                { _order1Id, _product5Id, 3 },
                { _order2Id, _product1Id, 2 },
                { _order3Id, _product2Id, 4 },
                { _order3Id, _product5Id, 7 },
                { _order3Id, _product6Id, 10 },
                { _order4Id, _product2Id, 3 },
                { _order4Id, _product5Id, 1 },
                { _order4Id, _product6Id, 10 },
                { _order5Id, _product7Id, 1 },
                { _order5Id, _product8Id, 1 }
            });

        public static void All(MigrationBuilder migrationBuilder)
        {
            ProductCategory(migrationBuilder);
            User(migrationBuilder);
            Product(migrationBuilder);
            Order(migrationBuilder);
            OrderProduct(migrationBuilder);
        }
    }
}
