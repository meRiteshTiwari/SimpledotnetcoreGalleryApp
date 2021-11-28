using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleImageGallery.Data.Migrations
{
    public partial class addedfieldforforeignkeyinGalleryImagemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GalleryImages_blobContainers_BlobContainerId",
                table: "GalleryImages");

            migrationBuilder.AlterColumn<int>(
                name: "BlobContainerId",
                table: "GalleryImages",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BlobContainrId",
                table: "GalleryImages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryImages_blobContainers_BlobContainerId",
                table: "GalleryImages",
                column: "BlobContainerId",
                principalTable: "blobContainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GalleryImages_blobContainers_BlobContainerId",
                table: "GalleryImages");

            migrationBuilder.DropColumn(
                name: "BlobContainrId",
                table: "GalleryImages");

            migrationBuilder.AlterColumn<int>(
                name: "BlobContainerId",
                table: "GalleryImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryImages_blobContainers_BlobContainerId",
                table: "GalleryImages",
                column: "BlobContainerId",
                principalTable: "blobContainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
