using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleImageGallery.Data.Migrations
{
    public partial class AddedBlobcontainerid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GalleryImages_blobContainers_BlobContainerId",
                table: "GalleryImages");

            migrationBuilder.DropIndex(
                name: "IX_GalleryImages_BlobContainerId",
                table: "GalleryImages");

            migrationBuilder.AlterColumn<int>(
                name: "BlobContainerId",
                table: "GalleryImages",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BlobContainerId",
                table: "GalleryImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_GalleryImages_BlobContainerId",
                table: "GalleryImages",
                column: "BlobContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryImages_blobContainers_BlobContainerId",
                table: "GalleryImages",
                column: "BlobContainerId",
                principalTable: "blobContainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
