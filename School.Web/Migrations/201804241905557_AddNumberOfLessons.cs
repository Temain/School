namespace School.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNumberOfLessons : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Disciplines", "NumberOfLessons", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Disciplines", "NumberOfLessons");
        }
    }
}
