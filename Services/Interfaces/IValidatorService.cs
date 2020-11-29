namespace ScheduleMaster.Services.Interfaces
{
    public interface IValidatorService
    {
        /// <summary>
        /// Функция для проверки объекта на валидность.
        /// </summary>
        /// <typeparam name="T">Тип объекта проверки</typeparam>
        /// <param name="obj">Объект проверки</param>
        /// <param name="error">Текст ошибки</param>
        /// <returns>Значение true, если проверка объекта завершена успешно; в противном случае — значение false.</returns>
        public bool Validate<T>(T obj, out string error);
    }
}