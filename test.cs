
protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.InsertData(
        table: "Ice",
        columns: new[] { "Id", "Name", "Ratio" },
    values: new object[,]
        {
                    { 1, "溫", -50 } ,
                    { 2, "熱", -100 } ,
                    { 3, "去冰", 0 } ,
                    { 4, "微冰", 20 } ,
                    { 5, "少冰", 50 } ,
                    { 6, "正常冰", 100} ,
                    { 7, "正常冰量", 900} ,
        }
    );
    migrationBuilder.InsertData(
        table: "Sugger",
        columns: new[] { "Id", "Name", "Ratio" },
        values: new object[,]
        {
                    { 1, "無糖", 0 } ,
                    { 2, "微糖", 10 } ,
                    { 3, "半糖", 30 } ,
                    { 4, "少糖", 50 } ,
                    { 5, "正常糖", 100 } ,
                    { 6, "正常甜度", 900} ,
        }
    );
    migrationBuilder.InsertData(
        table: "Topping",
        columns: new[] { "Id", "Name", "Price" },
        values: new object[,]
        {
                    { 1, "珍珠", 10 } ,
                    { 2, "仙草", 10 } ,
                    { 3, "綠茶凍", 10 } ,
                    { 4, "小芋圓", 15 } ,
                    { 5, "杏仁凍", 15} ,
                    { 6, "奶霜(限冷飲)", 20} ,
                    { 7, "太妃珍珠", 15} ,
                    { 8, "黑糖粉粿", 15} ,
        }
    );

    migrationBuilder.InsertData(
        table: "ServingSize",
        columns: new[] { "Id", "Name", "PriceGap" },
        values: new object[,]
        {
                    { 1, "M", 0 } ,
                    { 2, "L", 15 }
        });

    migrationBuilder.InsertData(
        table: "Store",
        columns: new[] { "Id", "Name", "Address", "MenuLink", "Telephone", "Memo" },
        values: new object[,]
        {
                    { 1, "五銅號 - 台北伊通店", "台北市", "https://order.nidin.shop/menu/11847","02-25070056", "" },
        });
    var now = DateTime.Now.ToTimestamp();
    Console.WriteLine(now);
    migrationBuilder.InsertData(
        table: "Drink",
        columns: new[] { "Id", "Name", "Price", "ToppingLowerLimit", "ToppingUpperLimit", "MenuLink", "Memo", "CreateTime", "UpdateTime" },
        values: new object[,]
         {
                    { 1, "春茶", 100, 0, 2, "", "", now,now },
                    { 2, "蜜茶", 200, 0, 2, "", "", now,now }
        });





    migrationBuilder.InsertData(
        table: "StoreDrinkRelation",
        columns: new[] { "StoreId", "DrinkId" },
        values: new object[,]
         {
                    { 1, 1 },
                    { 1, 2}
        }
    );
    migrationBuilder.InsertData(
                    table: "DrinkIceRelation",
                    columns: new[] { "DrinkId", "IceId" },
                    values: new object[,]
                     {
                    { 1, 1 },
                    { 1, 2},
                    { 1, 3 },
                    { 1, 4},
                    { 1, 5 },
                    { 1, 7},
                    { 2, 1 },
                    { 2, 2},
                    { 2, 3 },
                    { 2, 4},
                    { 2, 5},
                { 2, 6},
                    { 2, 7},
          }
        );

    migrationBuilder.InsertData(
    table: "DrinkSuggerRelation",
    columns: new[] { "DrinkId", "SuggerId" },
    values: new object[,]
     {
                    { 1, 1 },
                    { 1, 2},
                    { 1, 3 },
                    { 1, 4},
                    { 1, 5 },
                    { 1, 6},
                    { 2, 1 },
                    { 2, 2},
                    { 2, 3 },
                    { 2, 4},
                    { 2, 5},
                    { 2, 6},
  }
  );
    migrationBuilder.InsertData(
                    table: "DrinkToppingRelation",
                    columns: new[] { "DrinkId", "ToppingId" },
                    values: new object[,]
                     {
                    { 1, 1 },
                    { 1, 2},
                    { 1, 3 },
                    { 1, 4},
                    { 1, 5 },
                    { 1, 7},
                    { 2, 1 },
                    { 2, 2},
                    { 2, 3 },
                    { 2, 4},
                    { 2, 5},
                    { 2, 6},
                    { 2, 7},
          }
        );

    migrationBuilder
    .InsertData(table: "Permission", columns: new[] { "Id", "Name", "IsAdmin", "UpdateTime", "CreateTime" }, values: new object[,] {
                    { 1, "Admin", true,  now,now },
                    { 2, "User", false,  now,now }
  });


    migrationBuilder.InsertData(
        table: "Account",
        columns: new[] { "Id", "NickName", "Email", "ThumbnailUrl", "GoogleOpenId", "AccessToken", "Salt", "UpdateTime", "CreateTime", "PermissionId" },
        values: new object[,]
         {
                    { 1, "Admin", "Admin@bd.tw", "https://google.com", "0001", "token01", "salt01", now, now, 1 },
                    { 2, "User01", "User01@bd.tw", "https://google.com", "0002", "token02", "salt02", now, now, 2 },
        });







}
