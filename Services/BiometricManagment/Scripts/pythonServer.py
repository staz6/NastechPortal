import zmq
import csv
from zk import ZK, const

context = zmq.Context()
socket = context.socket(zmq.REP)
socket.bind("tcp://*:6666")
conn = None
while True:
	message = socket.recv()
	print("Received request: %s" % message)
	zk = ZK('10.1.0.70', port=4370, timeout=5,password=0, force_udp=False, ommit_ping=False)

	try:
		conn = zk.connect()
		conn.disable_device()
		attendance = conn.get_attendance()
		with open('attendanceLog.csv', 'w', newline='') as file:
			writer = csv.writer(file)
			for item in attendance:
				writer.writerow([item.user_id, item.timestamp])
		conn.enable_device()
	except Exception as e:
		print ("Process terminate : {}".format(e))
	finally:
		if conn:
			conn.disconnect()
	socket.send(b"Success")
