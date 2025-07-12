using capygram.Common.DTOs.Post;
using capygram.Common.DTOs.Share;
using capygram.Common.Services;
using capygram.Common.Shared;
using capygram.Newsfeed.Domain.Data;
using capygram.Newsfeed.Domain.Modal;
using capygram.Newsfeed.Domain.Repositories;
using Cassandra;
using System.Collections.Generic;

namespace capygram.Newsfeed.Repositories
{
    public class NewsfeedRepositories : INewsfeedRepositories
    {
        private INewsfeedContext _context;
        private IExternalService _externalService;
        public NewsfeedRepositories( INewsfeedContext context , IExternalService externalService ) {
            _context = context;
            _externalService = externalService;
        }
        public async Task AddNewsfeed( capygram.Newsfeed.Domain.Modal.Newsfeed feed )
        {
            var insertFeed = await _context.context.PrepareAsync(
            "INSERT INTO Newsfeed " +
            "(id, postId , userId, createdAt) " +
            "VALUES (uuid(), ?, ?, ?)");

            var createdAt = DateTime.Now;
            await _context.context.ExecuteAsync(insertFeed.Bind(feed.PostId,feed.UserId , DateTime.Now));
        }

        public async Task<PaginationDTO<PostDBDTO>> GetNewsfeedsByUserId(Guid UserId,int limit)
        {
            // Query to count total posts
            var countQuery = _context.context.Prepare("SELECT COUNT(*) FROM Newsfeed WHERE USERID = ?");
            var countResult = await _context.context.ExecuteAsync(countQuery.Bind(UserId));
            var totalPosts = countResult.First().GetValue<long>("count");
            //
            byte[] pagingState = null; // Trạng thái trang hiện tại, ban đầu là null để lấy trang đầu tiên

            // Chuẩn bị truy vấn
            var statement = new SimpleStatement("SELECT * FROM Newsfeed WHERE USERID = ?", UserId)
                                .SetPageSize(limit)
                                .SetPagingState(pagingState); 

            // Thực thi truy vấn và lấy kết quả
            var rowSet = await _context.context.ExecuteAsync(statement);

            // Lưu trạng thái trang để sử dụng cho lần truy vấn tiếp theo
            pagingState = rowSet.PagingState;

            // Xử lý dữ liệu ở đây
            var rows = rowSet.ToList();
            var newsfeeds = new List<Guid>();

            foreach (var row in rows)
            {
                newsfeeds.Add(row.GetValue<Guid>("postid"));
            }

            //get post by list postid 
            var posts = new List<PostDBDTO>();
            foreach( var postId in newsfeeds )
            {
                var post = await _externalService.GetExternalDataAsync<PostDBDTO>($"post:8081/api/Posts/Get/{postId}");
                if(post != null)
                {
                posts.Add(post);
                }    
            }
            //
            var pagination = new PaginationDTO<PostDBDTO>
            {
                Data = posts,
                total = (int)totalPosts,
                limit = limit,
            };

            return pagination;
        }
    }
}
