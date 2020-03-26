using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMXN.Data.Models;
using TMXN.Services.Data;
using TMXN.Web.ViewModels.Users;

namespace TMXN.Web.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(IUsersService usersService,UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
        }
        public IActionResult All()
        {
            var viewModel = new UserListViewModel
            {
                Users = this.usersService.GetAll<UserFriendViewModel>(),
            };

            return this.View(viewModel);

        }

        public async Task<IActionResult> Add(string id)
        {
            var myUser = await this.userManager.GetUserAsync(this.User);

           await this.usersService.AddAnotherUserToFriendlistAsync(id, myUser.Id);

            return this.RedirectToAction(nameof(AddToFriendlistSuccessful));
        }

        public IActionResult AddToFriendlistSuccessful()
        {
            return this.View();
        }

        public async Task<IActionResult> AllFriends()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            
            var viewModel = new FriendlistListViewModel
            {
                Friendlists = await this.usersService.AllFriendsAsync<UserFriendViewModel>(currentUser.Id),
            };


            return this.View(viewModel);
        }
    }
}
