<?php

	$host = "localhost";
	$username = "id4770849_workceo";
	$password = "delabEGO234";
	$db_name = "id4770849_work";
	$con = mysqli_connect( "$host", "$username", "$password", "$db_name" ) or die("cannot connect");
	
    $data2 = $_POST[ 'email' ];
    $data2 = strtolower($data2);
    $sql = "SELECT ID FROM workC WHERE Email = '".$data2."'";
    
    $result = mysqli_query( $con , $sql )
    or die ( "Error" );
    
    $json = array();
    
    if( mysqli_num_rows($result)){
    while($row = mysqli_fetch_row($result)){
    $json[] = $row;
    }
    }
    mysqli_close($con);
    
    if (count($json) == 0 ){
        die("Nothing2");
    }
    
    echo ("Something");

?>