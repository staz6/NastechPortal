import zmq
import csv
from zk import ZK, const

conn = None
zk = ZK('192.168.1.201', port=4370)

try:
		conn = zk.connect()
		conn.disable_device()
		# users = conn.get_users()
		# for user in users:
		# 	privilege = 'User'
		# 	if user.privilege == const.USER_ADMIN:
		# 		privilege = 'Admin'
		# 	print ('+ UID #{}'.format(user.uid))
		# 	print ('  Name       : {}'.format(user.name))
		# 	print ('  Privilege  : {}'.format(privilege))
		# 	print ('  Password   : {}'.format(user.password))
		# 	print ('  Group ID   : {}'.format(user.group_id))
		# 	print ('  User  ID   : {}'.format(user.user_id))
		# zk.enroll_user('1234')
		# conn.set_user(uid=2, name='Aahad', privilege=const.USER_ADMIN, password='12345678', group_id='', user_id='1234', card=0)
		# conn.set_user(uid=3, name='D',  password='123', group_id='', user_id='3', card=0)
		# conn.set_user(uid=4,name="Abdullah Mirza", privilege=const.USER_ADMIN,password='123',group_id='',user_id='4',card=0)
		# conn.set_user(uid=5,name="Shakir Ali",password='123',group_id='',user_id='5',card=0)
		# conn.set_user(uid=6,name="Salman junaid",password='123',group_id='',user_id='6',card=0)
		# conn.set_user(uid=7,name="Zain-ul-Abdin",password='123',group_id='',user_id='7',card=0)
		# conn.set_user(uid=8,name="Yumeena Ahmed",password='123',group_id='',user_id='8',card=0)
		# conn.set_user(uid=9,name="Maryum Sohail",password='123',group_id='',user_id='9',card=0)
		# conn.set_user(uid=10,name="Hyder Ali Maroof",password='123',group_id='',user_id='10',card=0)
		attendance = conn.get_attendance()
		
		print(attendance);
		
		unlock = conn.unlock()
		print(unlock)
		conn.enable_device()

except Exception as e:
		print ("Process terminate : {}".format(e))
finally:
		if conn:
			conn.disconnect()