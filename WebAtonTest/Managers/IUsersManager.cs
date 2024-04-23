using WebAtonTest.Data;
using WebAtonTest.Requests;
using WebAtonTest.Responses;
namespace WebAtonTest.Managers
{
    public interface IUsersManager
    {
        //Create
        /// <summary>
        /// 1) Создание пользователя по логину, паролю, имени, полу и дате рождения + указание будет ли 
        /// пользователь админом(Доступно Админам)
        /// </summary>
        /// <param name="executorDataRequest"></param>
        /// <param name="createUserRequest"></param>
        /// <returns></returns>
        Task<User> CreateUser(ExecutorDataRequest executorDataRequest,CreateUserRequest createUserRequest);
        //Update-1
        /// <summary>
        /// 2) Изменение имени пользователя (Может менять Администратор, либо 
        /// лично пользователь, если он активен(отсутствует RevokedOn))
        /// </summary>
        /// <param name="executorDataRequest"></param>
        /// <param name="UserLogin"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        Task<User> UpdateUserData(ExecutorDataRequest executorDataRequest, string UserLogin, string newName);
        /// <summary>
        /// 2) Изменение пола пользователя (Может менять Администратор, либо 
        /// лично пользователь, если он активен(отсутствует RevokedOn))
        /// </summary>
        /// <param name="executorDataRequest"></param>
        /// <param name="UserLogin"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        Task<User> UpdateUserData(ExecutorDataRequest executorDataRequest, string UserLogin, int newGender);
        /// <summary>
        /// 2) Изменение даты рождения пользователя (Может менять Администратор, либо 
        /// лично пользователь, если он активен(отсутствует RevokedOn))
        /// </summary>
        /// <param name="executorDataRequest"></param>
        /// <param name="UserLogin"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        Task<User> UpdateUserData(ExecutorDataRequest executorDataRequest, string UserLogin, DateTime newBirthday);
        /// <summary>
        /// 3) Изменение пароля (Пароль может менять либо Администратор, либо лично пользователь, если
        ///он активен(отсутствует RevokedOn))
        /// </summary>
        /// <param name="executorDataRequest"></param>
        /// <param name="UserLogin"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task<User> UpdateUserPassword(ExecutorDataRequest executorDataRequest, string UserLogin, string newPassword);
        /// <summary>
        /// 4) Изменение логина (Логин может менять либо Администратор, либо лично пользователь, если
        /// он активен(отсутствует RevokedOn), логин должен оставаться уникальным)
        /// </summary>
        /// <param name="executorDataRequest"></param>
        /// <param name="UserLogin"></param>
        /// <param name="newLogin"></param>
        /// <returns></returns>
        Task<User> UpdateUserLogin(ExecutorDataRequest executorDataRequest, string UserLogin, string newLogin);
        //Read
        /// <summary>
        /// 5) Запрос списка всех активных (отсутствует RevokedOn) пользователей, список отсортирован по
        ///CreatedOn(Доступно Админам)
        /// </summary>
        /// <param name="executorDataRequest"></param>
        /// <returns></returns>
        Task<List<User>> GetUsers(ExecutorDataRequest executorDataRequest);
        /// <summary>
        /// 6) Запрос пользователя по логину, в списке долны быть имя, пол и дата рождения статус активный
        ///или нет(Доступно Админам)
        /// </summary>
        /// <param name="executorDataRequest"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<GetUserByLoginResponse> GetUserByLogin(ExecutorDataRequest executorDataRequest, string login);
        /// <summary>
        /// 7) Запрос пользователя по логину и паролю (Доступно только самому пользователю, если он
        /// активен(отсутствует RevokedOn))
        /// </summary>
        /// <param name="executorDataRequest"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User> GetUserByLoginAndPassword(ExecutorDataRequest executorDataRequest, string login, string password);
        /// <summary>
        /// 8) Запрос всех пользователей старше определённого возраста (Доступно Админам)
        /// </summary>
        /// <param name="executorDataRequest"></param>
        /// <param name="Age"></param>
        /// <returns></returns>
        Task<List<User>> GetUsersOlderThen(ExecutorDataRequest executorDataRequest, int age);
        //Delete
        /// <summary>
        /// 9) Удаление пользователя по логину полное или мягкое (При мягком удалении должна
        ///происходить простановка RevokedOn и RevokedBy) (Доступно Админам)
        /// </summary>
        /// <param name="executorDataRequest"></param>
        /// <param name="Login"></param>
        /// <returns></returns>
        Task<User> RevokeUser(ExecutorDataRequest executorDataRequest, string login);
        //Update-2
        /// <summary>
        /// 10) Восстановление пользователя - Очистка полей (RevokedOn, RevokedBy) (Доступно Админам)
        /// </summary>
        /// <param name="executorDataRequest"></param>
        /// <returns></returns>
        Task<User> RestoreUser(ExecutorDataRequest executorDataRequest, string login);

    }
}
