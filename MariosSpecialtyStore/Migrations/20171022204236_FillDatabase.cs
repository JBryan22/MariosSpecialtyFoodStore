using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MariosSpecialtyStore.Migrations
{
    public partial class FillDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO products (Name, Cost, Country) VALUES ('Pepperoni', 12.75, 'USA')");
            migrationBuilder.Sql("INSERT INTO products (Name, Cost, Country) VALUES ('Brie', 17.50, 'Italy')");
            migrationBuilder.Sql("INSERT INTO products (Name, Cost, Country) VALUES ('Pinot Noir', 47.50, 'USA')");
            migrationBuilder.Sql("INSERT INTO products (Name, Cost, Country) VALUES ('Champagne', 33.75, 'France')");
            migrationBuilder.Sql("INSERT INTO products (Name, Cost, Country) VALUES ('Salami', 12.99, 'Italy')");

            migrationBuilder.Sql("INSERT INTO reviews (Author, Content_Body, Rating, ProductId) VALUES ('Jesse', 'Maecenas interdum fringilla ipsum nec laoreet. Etiam ultricies felis non massa euismod rhoncus. Donec viverra tincidunt ipsum, in aliquet mauris viverra et. Praesent at quam metus. Nunc at laoreet tellus, sed tincidunt lectus. Pellentesque consectetur est magna, sit amet facilisis tellus scelerisque vitae. Ut ornare, tortor eget laoreet luctus, dui justo congue justo, non tempor libero neque sed mauris. Fusce magna lacus, sollicitudin eu tempor non, fringilla eu mauris.', 5, (SELECT ProductId FROM products WHERE Name = 'Pepperoni'))");
            migrationBuilder.Sql("INSERT INTO reviews (Author, Content_Body, Rating, ProductId) VALUES ('Kaili', 'Maecenas interdum fringilla ipsum nec laoreet. Etiam ultricies felis non massa euismod rhoncus. Donec viverra tincidunt ipsum, in aliquet mauris viverra et. Praesent at quam metus. Nunc at laoreet tellus, sed tincidunt lectus. Pellentesque consectetur est magna, sit amet facilisis tellus scelerisque vitae. Ut ornare, tortor.', 3, (SELECT ProductId FROM products WHERE Name = 'Pepperoni'))");
            migrationBuilder.Sql("INSERT INTO reviews (Author, Content_Body, Rating, ProductId) VALUES ('Charlie', 'Maecenas interdum fringilla ipsum nec laoreet. Etiam ultricies felis non massa euismod rhoncus. Donec viverra tincidunt ipsum, in aliquet mauris viverra et. Praesent at quam metus. Nunc at laoreet tellus, sed tincidunt lectus. Pellentesque consectetur est magna, sit amet facilisis tellus scelerisque vitae. Ut ornare, tortor eget laoreet luctus, dui justo cons, sollicitudin eu tempor non, fringilla eu mauris.', 1, (SELECT ProductId FROM products WHERE Name = 'Pepperoni'))");
            migrationBuilder.Sql("INSERT INTO reviews (Author, Content_Body, Rating, ProductId) VALUES ('Jesse', 'Maecenas interdum fringilla ipsum nec laoreet. Etiam ultricies felis non massa euismod rhoncus. Donec viverra tincidunt ipsum, in aliquet mauris viverra et. Praesent at quam metus. Nunc at laoreet tellus, sed tincidunt lectus. Pellentesque consectetur est magna, sit amet facilisis tellus scelerisque vitae. Ut ornare, tortor eget laoreet luctus, dui justo conicitudin eu tempor non, fringilla eu mauris.', 4, (SELECT ProductId FROM products WHERE Name = 'Brie'))");
            migrationBuilder.Sql("INSERT INTO reviews (Author, Content_Body, Rating, ProductId) VALUES ('Jesse', 'Maecenas interdum fringilla ipsum nec.', 3, (SELECT ProductId FROM products WHERE Name = 'Brie'))");
            migrationBuilder.Sql("INSERT INTO reviews (Author, Content_Body, Rating, ProductId) VALUES ('Jesse', 'Maecenas interdum fringilla ipsum nec.  convallis, nisi viverra tincidunt al', 4, (SELECT ProductId FROM products WHERE Name = 'Pinot Noir'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
