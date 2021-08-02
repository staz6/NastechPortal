import zmq
import csv
from zk import ZK, const

conn = None
zk = ZK('10.1.0.70', port=4370)

try:
		conn = zk.connect()
		conn.disable_device()
		users = conn.get_users()
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
		# # conn.set_user(uid=2, name='Aahad', privilege=const.USER_ADMIN, password='12345678', group_id='', user_id='1234', card=0)
		unlock = conn.unlock()
		print(unlock)
		conn.enable_device()

except Exception as e:
		print ("Process terminate : {}".format(e))
finally:
		if conn:
			conn.disconnect()