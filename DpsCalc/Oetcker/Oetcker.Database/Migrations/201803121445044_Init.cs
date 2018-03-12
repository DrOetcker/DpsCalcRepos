namespace Oetcker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemIdent = c.Int(nullable: false),
                        DmgMax = c.Double(nullable: false),
                        Speed = c.Double(nullable: false),
                        DisplayIdent = c.Int(nullable: false),
                        Name = c.String(),
                        DmgMin = c.Double(nullable: false),
                        WeaponClass = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Quality = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemIdent);
            
            CreateTable(
                "dbo.Spells",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EffectBasePoints = c.Int(nullable: false),
                        EffectAura = c.Int(nullable: false),
                        Name = c.String(),
                        SpellIdent = c.Int(nullable: false),
                        Item_ItemIdent = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.Item_ItemIdent)
                .Index(t => t.Item_ItemIdent);
            
            CreateTable(
                "dbo.PlayerItemSets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Class = c.Int(nullable: false),
                        Race = c.Int(nullable: false),
                        CurrentItemSet_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.PlayerItemSets", t => t.CurrentItemSet_Id)
                .Index(t => t.CurrentItemSet_Id);
            
            CreateTable(
                "dbo.PlayerItemSetItems",
                c => new
                    {
                        PlayerItemSet_Id = c.Guid(nullable: false),
                        Item_ItemIdent = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayerItemSet_Id, t.Item_ItemIdent })
                .ForeignKey("dbo.PlayerItemSets", t => t.PlayerItemSet_Id, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.Item_ItemIdent, cascadeDelete: true)
                .Index(t => t.PlayerItemSet_Id)
                .Index(t => t.Item_ItemIdent);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "CurrentItemSet_Id", "dbo.PlayerItemSets");
            DropForeignKey("dbo.PlayerItemSetItems", "Item_ItemIdent", "dbo.Items");
            DropForeignKey("dbo.PlayerItemSetItems", "PlayerItemSet_Id", "dbo.PlayerItemSets");
            DropForeignKey("dbo.Spells", "Item_ItemIdent", "dbo.Items");
            DropIndex("dbo.PlayerItemSetItems", new[] { "Item_ItemIdent" });
            DropIndex("dbo.PlayerItemSetItems", new[] { "PlayerItemSet_Id" });
            DropIndex("dbo.Players", new[] { "CurrentItemSet_Id" });
            DropIndex("dbo.Spells", new[] { "Item_ItemIdent" });
            DropTable("dbo.PlayerItemSetItems");
            DropTable("dbo.Players");
            DropTable("dbo.PlayerItemSets");
            DropTable("dbo.Spells");
            DropTable("dbo.Items");
        }
    }
}
