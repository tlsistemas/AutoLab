namespace AutoLab.Utils.Events
{
    public class Events
    {
        #region Mensages Assemblage
        public static readonly Event INVALID_VALUE = new Event(nameof(INVALID_EMPTY), "O valor do campo {0} já esta cadastrado.");

        #endregion

        #region Error Messages
        public static readonly Event SYSTEM_ERROR_NOT_HANDLED = new Event(nameof(SYSTEM_ERROR_NOT_HANDLED), "Não foi possível processar a requisição.");
        public static readonly Event SYSTEM_ERROR_NOT_FOUND = new Event(nameof(SYSTEM_ERROR_NOT_FOUND), "{0} não foi encontrado.");
        #endregion

        #region Validation Messages
        public static readonly Event INVALID_EMPTY = new Event(nameof(INVALID_EMPTY), "Campo {0} é obrigatório.");
        public static readonly Event INVALID_PARTICIPANT_NAME = new Event(nameof(INVALID_PARTICIPANT_NAME), "Para cadastrar os dados precisamos do seu nome completo.");
        public static readonly Event INVALID_PARTICIPANT_AGE = new Event(nameof(INVALID_PARTICIPANT_AGE), "Você precisa ter mais de 18 anos para participar da promoção.");
        public static readonly Event INVALID_DOCUMENT_NUMBER = new Event(nameof(INVALID_DOCUMENT_NUMBER), "Ops! Esse não parece ser um número de CPF válido.");
        public static readonly Event INVALID_CNPJ = new Event(nameof(INVALID_CNPJ), "Ops! Esse não parece ser um número de CNPJ válido.");
        public static readonly Event INVALID_EMAIL_ADDRESS = new Event(nameof(INVALID_EMAIL_ADDRESS), "Ops! Esse não parece ser um e-mail válido.");
        public static readonly Event INVALID_PHONE_NUMBER = new Event(nameof(INVALID_PHONE_NUMBER), "Ops! Esse não parece ser um celular válido.");
        public static readonly Event INVALID_PASSWORD = new Event(nameof(INVALID_PASSWORD), "Ops! a senha digitada não possui um dos itens solicitados acima.");
        public static readonly Event EXISTIN_PARTICIPANT = new Event(nameof(EXISTIN_PARTICIPANT), "Seu CPF já está cadastrado.");
        public static readonly Event INVALID_PARTICIPANT_BIRTH_DATE = new Event(nameof(INVALID_PARTICIPANT_BIRTH_DATE), "Para cadastrar os dados precisamos da sua data de nascimento.");
        public static readonly Event ERROR_USER_NOT_FOUND = new Event(nameof(ERROR_USER_NOT_FOUND), "Usuário não encontrado.");

        public static readonly Event ERROR_AUTHENTICATE_EXCEPTION = new Event(nameof(ERROR_AUTHENTICATE_EXCEPTION), "Ocorreu algum problema na autenticação, por favor tente novamente mais tarde!");
        public static readonly Event ERROR_AUTHENTICATE_PASSWORD_USER_INVALID = new Event(nameof(ERROR_AUTHENTICATE_PASSWORD_USER_INVALID), "Login e/ou senha inválido!");

        public static readonly Event CHANGE_PASSWORD_USER_ERROR = new Event(nameof(CHANGE_PASSWORD_USER_ERROR), "Erro ao editar as informações, tente novamente mais tarde!");
        public static readonly Event ERROR_CHANGE_PASSWORD_INVALID_FORMAT = new Event(nameof(ERROR_CHANGE_PASSWORD_INVALID_FORMAT), "A senha deve conter no mínimo 8 e no máximo 32 caracteres, sendo letras e números");
        public static readonly Event ERROR_CHANGE_PASSWORD_OLDPASSWORD = new Event(nameof(ERROR_CHANGE_PASSWORD_OLDPASSWORD), "Senha atual incorreta");
        public static readonly Event ERROR_CHANGE_PASSWORD_CONFIRM = new Event(nameof(ERROR_CHANGE_PASSWORD_CONFIRM), "A senha digitada está diferente, digite novamente.");
        public static readonly Event CHANGE_PASSWORD_USER_SUCCESS = new Event(nameof(CHANGE_PASSWORD_USER_SUCCESS), "Senha atualizada com sucesso!");

        public static readonly Event ERROR_FORGET_PASSWORD_USER_INVALID = new Event(nameof(ERROR_FORGET_PASSWORD_USER_INVALID), "Usuário não encontrado");
        public static readonly Event FORGET_PASSWORD_PARTICIPANT_NOT_FOUND = new Event(nameof(FORGET_PASSWORD_PARTICIPANT_NOT_FOUND), "O CPF informado não está cadastrado.");
        public static readonly Event FORGET_PASSWORD_USER_SUCCESS = new Event(nameof(FORGET_PASSWORD_USER_SUCCESS), "Foi enviada uma nova senha de acesso para o e-mail cadastrado");


        public static readonly Event GET_PARTICIPANT_DETAILS_ERROR = new Event(nameof(GET_PARTICIPANT_DETAILS_ERROR), "Não foi possível obter detalhes do participante pelo CPF '{0}'.");
        public static readonly Event CREATE_PARTICIPANT_ERROR = new Event(nameof(CREATE_PARTICIPANT_ERROR), "Não foi possível executar a operação, verifique o campo '{0}'.");
        #endregion

        public static readonly Event GET_ERROR_GENERIC = new Event(nameof(GET_ERROR_GENERIC), "Não foi possível recuperar os registros para {0}.");
        public static readonly Event GET_ONE_ERROR_GENERIC = new Event(nameof(GET_ONE_ERROR_GENERIC), "Não foi possível recuperar o registro {0}.");
        public static readonly Event CREATE_ERROR_GENERIC = new Event(nameof(CREATE_ERROR_GENERIC), "Não foi possível criar o/a entidade {0} para {1}.");
        public static readonly Event UPDATE_ERROR_GENERIC = new Event(nameof(UPDATE_ERROR_GENERIC), "Não foi possível editar o/a entidade {0} para {1}.");
        public static readonly Event DELETE_ERROR_GENERIC = new Event(nameof(DELETE_ERROR_GENERIC), "Não foi possível deletar o/a entidade {0} para o ID {1}.");

        public static readonly Event EXIST_ENTITY_GENERIC = new Event(nameof(EXIST_ENTITY_GENERIC), "Já existe um registro cadastrado para {0}.");
    }
    public struct Event
    {
        public String Name { get; }
        public String Message { get; }

        public Event(String name, String message)
        {
            Name = name;
            Message = message;
        }
    }
}
