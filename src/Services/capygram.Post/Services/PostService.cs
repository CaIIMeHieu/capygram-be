using capygram.Common.DTOs.Post;
using capygram.Common.DTOs.Share;
using capygram.Common.Exceptions;
using capygram.Common.MessageBus.Command;
using capygram.Common.MessageBus.Events;
using capygram.Common.Shared;
using capygram.Post.Domain.Model;
using capygram.Post.Domain.Repositories;
using MassTransit;

namespace capygram.Post.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepositories _postRepositories;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IBus _bus;
        public PostService( IPostRepositories postRepositories , IPublishEndpoint publishEndpoint , IBus bus) {
            _publishEndpoint = publishEndpoint;
            _postRepositories = postRepositories;
            _bus = bus;
        }
        public async Task<Result<string>> CreatePostAsync(PostDTO newPosts)
        {
            if (newPosts == null) {
               throw new BadRequestException("Bài viết không hợp lệ");
            }
            #region Mapper
            var post = new Posts();
            post.Id = Guid.NewGuid();
            post.CreatedAt = DateTime.Now;
            post.Content = newPosts.Content;
            post.UpdateAt = DateTime.Now;
            post.UserName = newPosts.UserName;
            post.UserId = newPosts.UserId;
            post.Likes = newPosts.Likes;
            post.UserAvartar = newPosts.UserAvartar;
            #endregion
            await _postRepositories.CreatePostAsync(post);
            //message to media service to upload image
            using var source = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            var dataUrl = await ConvertListIFormFileToListByte.Convert(newPosts.ImageUrls);
            await _publishEndpoint.Publish(new ImageUploadNotification {
                Id = post.Id,
                Data = dataUrl,
                Type = NotificationMediaType.post,
                Name = "Message upload image" , 
                Description = "Raise Message to Media service",
                CloudinaryFolder = CloudinaryFolder.Capygram_Post,
            }, source.Token);
            //get post by id 
            var p = await _postRepositories.GetPostByIdAsync(post.Id);
            PostCreatedDTO pdto = new PostCreatedDTO
            {
                PostId = p.Id,
                CreatedAt = p.CreatedAt ,
                UserId = p.UserId,
            };
            var postCreatedNotification = new PostCreatedNotification{ 
                Id = post.Id,
                Data = pdto ,
                Type = "post",
                Description = "wait to insert post to cassandra" ,
                Name = "insert" ,
                TimeStamp = DateTimeOffset.Now,
            };
            var enpoint = await _bus.GetSendEndpoint(Address<PostCreatedNotification>());
            await enpoint.Send(postCreatedNotification, source.Token);
            return Result<string>.CreateResult(true, new ResultDetail("200", "Success"), "Create post successfully");
        }

        public async Task<Result<string>> DeletePostByIdAsync(Guid PostID)
        {
            if (PostID == null)
            {
                throw new BadRequestException();
            }
            await _postRepositories.DeletePostByIdAsync(PostID);
            return Result<string>.CreateResult(true, new ResultDetail("200", "Success"), "Delete post successfully");
        }

        public async Task<Result<Posts>> GetPostByIdAsync(Guid PostID)
        {
            if( PostID == null )
            {
                throw new BadRequestException();
            }
            var post = await _postRepositories.GetPostByIdAsync(PostID);
            return Result<Posts>.CreateResult(true, new ResultDetail("200", "Get post successfully") ,post);
        }

        public async Task<Result<List<Posts>>> GetPostByUserIdAsync(Guid UserID)
        {
            var posts = await _postRepositories.GetPostByUserIdAsync(UserID);
            return Result<List<Posts>>.CreateResult(true, new ResultDetail("200", "Get post successfully"), posts);
        }

        public async Task<Result<PaginationDTO<Posts>>> GetPostsAsync(int pageSize, int pageNumber)
        {            
            var posts = await _postRepositories.GetPostsAsync(pageSize, pageNumber);
            long total = await _postRepositories.GetTotalPosts();
            var pagination = new PaginationDTO<Posts>
            {
                Data = posts,
                limit = pageSize,
                page = pageNumber,
                total = total
            };
            return Result<PaginationDTO<Posts>>.CreateResult(true, new ResultDetail("200", "Get post successfully"), pagination);
        }

        public async Task<Result<string>> UpdatePostByIdAsync(PostUpdateDTO post)
        {
            if (post == null)
            {
                throw new BadRequestException("Bài viết không hợp lệ");
            }
            var p = new Posts();
            p.Id = post.Id;
            p.UpdateAt = DateTime.Now;
            p.UserName = post.UserName;
            p.UserId = post.UserId;
            p.Content = post.Content;
            p.ImageUrls = post.ImageUrls;
            p.Likes = post.Likes;

            await _postRepositories.UpdatePostByIdAsync(p);
            return Result<string>.CreateResult(true, new ResultDetail("200", "Success"), "Update post successfully");
        }
        private static Uri Address<T>()
            => new($"queue:{KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(T).Name)}");
    }
}
