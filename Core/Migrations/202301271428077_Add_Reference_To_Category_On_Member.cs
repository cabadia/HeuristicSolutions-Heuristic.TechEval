namespace Heuristics.TechEval.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Reference_To_Category_On_Member : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Member", "Category_Id", c => c.Int());
            CreateIndex("dbo.Member", "Category_Id");
            AddForeignKey("dbo.Member", "Category_Id", "dbo.Category", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Member", "Category_Id", "dbo.Category");
            DropIndex("dbo.Member", new[] { "Category_Id" });
            DropColumn("dbo.Member", "Category_Id");
        }
    }
}
