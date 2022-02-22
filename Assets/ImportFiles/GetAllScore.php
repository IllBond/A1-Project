<?php
$mysqli = new mysqli('localhost','root','','a1');
$myArray = array();
if ($result = $mysqli->query("SELECT * FROM `userdata` ORDER BY `userdata`.`score` DESC;")) {

    while($row = $result->fetch_array(MYSQLI_ASSOC)) {
            $myArray[] = $row;
    }
    
}

$result->close();
$mysqli->close();

$json = $myArray;

$all = json_encode(array(
  "users" => $json
), JSON_UNESCAPED_UNICODE);

echo $all ;


$file = fopen('RequestMe.json','w+');
fwrite($file, $all);
fclose($file);

?>