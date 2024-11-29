using System;
using UsersMicroservice.Entities;

namespace UsersMicroservice.Repositories
{
    public interface IUsers
    {
        //Login
        Task<LoginResponseModel> UserLogIn(LoginModel model);
        Task<string> GetPasswordHash(string Email);

        List<Menu> MenuDinamico(int RoleID);

        //Metodos GET
        Task<IEnumerable<UsersList>> UsersList();
        Task<UsersList> UsersListById(int UserID);
        Task<IEnumerable<UsersList>> UsersByRole(int RoleID);
        Task<IEnumerable<ClientsList>> ClientsList();
        Task<IEnumerable<RepresentativesList>> RepresentativesList();

        Task<UserInfoModel> InfoClient(int ClientID);
        Task<UserInfoModel> InfoRep(int RepID);

        Task<IEnumerable<ClientIDModel>> ClientID(int ClientID);

        //Métodos POST
        Task RegisterUser(RegisterUserModel model);


        //Métodos PUT
        Task UpdateUser(int UserID, string Name, string Email, string Password, string PhoneNumber);


        //Métodos DELETE
        Task DeleteUser(int UserID);
    }
}
