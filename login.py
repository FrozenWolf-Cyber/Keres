import mysql.connector

class login:
    def __init__(self,host,user,passwd,database):
        self.db = mysql.connector.connect(host=host,
                                     user=user,
                                     passwd=passwd,
                                     database=database)


        self.cursor = self.db.cursor()

    def check_credentials_check(self,data,user_name_or_mail_id):
        a = ''
        #pswrd incorrect - 0
        pswrd_correct = 0
        if user_name_or_mail_id == 'username' :
            self.cursor.execute('SELECT id , password FROM PLAYER_LOGIN WHERE user_name = \''+ data[0] + '\'' )
            for i in self.cursor:
                a = i

        if user_name_or_mail_id == 'mail_id' :
            self.cursor.execute('SELECT id , password  FROM PLAYER_LOGIN WHERE mail_id = \'' + data[0] + '\'')
            for i in self.cursor:
                a = i

        if len(a) == 2:
            if a[1] == data[1]:
                return a[0]
            else :
                return  pswrd_correct

        else:
            #username doesnt exist
            return 3


    def player_login_details(self,data,type_of_login):
        user_name_or_mail_id , password = data[0] , data[1]

        if type_of_login == 'username':
            response = self.check_credentials_check((user_name_or_mail_id,password),'username')

        else:
            response = self.check_credentials_check((user_name_or_mail_id,password),'mail_id')

        return response




