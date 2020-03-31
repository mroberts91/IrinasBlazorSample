using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IrinasBlazorSample.Data;
using Microsoft.AspNetCore.Components;
using IrinasBlazorSample.Data.Models;
using IrinasBlazorSample.Data;
using Microsoft.EntityFrameworkCore;

namespace IrinasBlazorSample.Pages
{
    public partial class Events
    {
        [Inject] DataContext DataContext { get; set; }
        User SelectedUser { get; set; }
        List<User> AvailableUsers { get; set; } = new List<User>();

        protected override async Task OnInitializedAsync()
        {
            LoadUsers();
        }

        void LoadUsers()
        {
            if (DataContext.Users.Count() > 0)
            {
                AvailableUsers = DataContext.Users.Include(u => u.Posts).ToList();
            }
        }

        void OnSelectedUserChange(User user)
        {
            SelectedUser = user;
        }

        void OnSelectedUserPostAdd(Post post)
        {
            var user = DataContext.Users.Where(u => u.Id == SelectedUser.Id).Include(u => u.Posts).FirstOrDefault();
            if (user is null || post is null) return;

            user.Posts.Add(post);
            DataContext.SaveChanges();
            
        }
    }
}
