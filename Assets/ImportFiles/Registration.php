<?php

include('config.php');

 $_baza = mysqli_query($connections, "INSERT INTO `userdata` (
 	`id`, 
 	`name`, 
 	`lastname`, 
 	`email`, 
 	`password`, 
	`score`, 
 	`identifier`) VALUES (
 	NULL, 
 	'".$_POST['name']."', 
 	'".$_POST['lastname']."', 
 	'".$_POST['email']."', 
 	'".$_POST['password']."', 
 	'".$_POST['score']."', 
 	'".$_POST['identifier']."')");  

?>