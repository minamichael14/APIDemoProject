using APIDay1.DTO;
using APIDay1.Helpers;
using APIDay1.Models;
using APIDay1.Repository;

namespace APIDay1.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _repository;
        private readonly IRepository<Role> _roleRepository;
        public AuthService(IRepository<User> repository, IRepository<Role> RoleRepository)
        {
            _repository = repository;
            _roleRepository = RoleRepository;
        }

        public RequestResult<bool> Register(UserDto user)
        {
            var userExist =  _repository.GetAll(x => x.email == user.email).Any();
            if (userExist)
            {
                return RequestResult<bool>.Failure("User email already exists");
            }

            _repository.Add(user.ToModel());
            return RequestResult<bool>.Success(true);
        }

        public RequestResult<string> Login(LoginDTO loginDTO)
        {
           var userExist = _repository.GetAll(x=>x.email == loginDTO.Email && x.password == loginDTO.Password).Any();
            if (!userExist)
            {
                return RequestResult<string>.Failure("Either emails or password are incorrect");
            }
            var user = _repository.GetAll(x => x.email == loginDTO.Email && x.password == loginDTO.Password).FirstOrDefault();
            
            var token = TokenHelper.GenerateToken(user.ID, user.UserName, GetRoleName(user.RoleID));
            return RequestResult<string>.Success(token);
        }

        public RequestResult<int> AddRole(RoleDto roleDTO)
        {
            var role = new Role
            {
                ID = roleDTO.ID,
                RoleName = roleDTO.Name
            };
            _roleRepository.Add(role);
            return RequestResult<int>.Success(role.ID);

        }

        public string GetRoleName(int roleID)
        {
           return _roleRepository.GetAll(x=>x.ID == roleID).Select(x=>x.RoleName).FirstOrDefault();
        }
    }
}
