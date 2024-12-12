using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using MyList.Controllers;
using MyList.Models;
using Xunit;

namespace MyList.Tests
{
    public class GiftListsControllerTests
    {
        private readonly TestGiftListContext _context;
        private readonly GiftListsController _controller;

        public GiftListsControllerTests()
        {
            var connectionString = "DataSource=:memory:";
            var options = new DbContextOptionsBuilder<TestGiftListContext>()
                .UseSqlite(connectionString)
                .Options;

            _context = new TestGiftListContext(options);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["ApiKey"]).Returns("mylist_api_key");

            _controller = new GiftListsController(_context, configurationMock.Object);
            
            _context.GiftLists.RemoveRange(_context.GiftLists);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetGiftLists_ReturnsOkResult()
        {
            _context.GiftLists.Add(new GiftList { 
                ListName = "Test List 1", 
                GiftName = "Test Gift 1",  
                GiftDescription = "Test Description 1"
            });
            _context.GiftLists.Add(new GiftList { 
                ListName = "Test List 2", 
                GiftName = "Test Gift 2", 
                GiftDescription = "Test Description 2"
            });
            await _context.SaveChangesAsync();

            var result = await _controller.GetGiftLists("mylist_api_key");
            
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var giftLists = Assert.IsAssignableFrom<IEnumerable<GiftList>>(okResult.Value);
            Assert.Equal(2, giftLists.Count());
        }

        [Fact]
        public async Task GetGiftList_ExistingId_ReturnsOkResult()
        {
            var giftList = new GiftList { 
                ListName = "Test List 1", 
                GiftName = "Test Gift",
                GiftDescription = "Test Description"
            }; 
            _context.GiftLists.Add(giftList);
            await _context.SaveChangesAsync();
            var giftListId = giftList.Id;
            
            var result = await _controller.GetGiftList(giftListId, "mylist_api_key");
            
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedGiftList = Assert.IsType<GiftList>(okResult.Value);
            Assert.Equal(giftListId, returnedGiftList.Id);
        }

        [Fact]
        public async Task GetGiftList_NonExistingId_ReturnsNotFound()
        {
            var result = await _controller.GetGiftList(999,"mylist_api_key");
            
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostGiftList_ValidObject_ReturnsCreatedAtAction()
        {
            var newGiftList = new GiftList { 
                ListName = "New Test List", 
                GiftName = "Test Gift",
                GiftDescription = "Test Description"
            };
            
            var result = await _controller.PostGiftList(newGiftList,"mylist_api_key");
            
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedGiftList = Assert.IsType<GiftList>(createdAtActionResult.Value);
            Assert.Equal(newGiftList.ListName, returnedGiftList.ListName);
        }

        [Fact]
        public async Task DeleteGiftList_ExistingId_ReturnsNoContent()
        {
            var giftList = new GiftList { 
                ListName = "Test List 1", 
                GiftName = "Test Gift",
                GiftDescription = "Test Description"
            };
            _context.GiftLists.Add(giftList);
            await _context.SaveChangesAsync();
            var giftListId = giftList.Id;
            
            var result = await _controller.DeleteGiftList(giftListId,"mylist_api_key");
            
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteGiftList_NonExistingId_ReturnsNotFound()
        {
            var result = await _controller.DeleteGiftList(999,"mylist_api_key");
            
            Assert.IsType<NotFoundResult>(result);
        }
    }
}