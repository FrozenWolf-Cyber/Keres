import mysql.connector

class master_control:
    def __init__(self,host,user,passwd,database):
        self.db = mysql.connector.connect(host=host,
                                     user=user,
                                     passwd=passwd,
                                     database=database)


        self.cursor = self.db.cursor()


    def action_to_do(self,action_type,query,value="0"):
        data = ""
        if action_type == "read":
            temp = []
            self.cursor.execute(query)
            for i in self.cursor:
                temp.append(i)
            data = str(temp)

        if action_type == "modify":
            self.cursor.execute(query,value)
            data = "DONE"

        return data
