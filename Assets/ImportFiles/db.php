<?php
$connections = mysqli_connect(
	$config['db']['server'],
	$config['db']['username'],
	$config['db']['password'],
	$config['db']['name']);

if ($connections == false) {
	echo "Ошибка подключения";
	echo mysqli_connect_error();
	exit();
}
session_start();

?>