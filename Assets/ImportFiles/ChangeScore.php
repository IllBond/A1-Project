<?php

include('config.php');

 $_baza = mysqli_query($connections, 
 	"UPDATE `userdata` SET `score` = '".$_POST['score']."' WHERE `userdata`.`id` = '".$_POST['identifier']."'";  
?>