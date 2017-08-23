pragma solidity^0.4.0;

contract InvestorDividendContract {
    
    mapping(address => uint) _shares;

    address _simonAddress = 0x4b0897b0513fdc7c541b6d9d7e929c4e5364d2db;
    address _pawelAddress = 0x14723a09acff6d2a60dcdf7aa4aff308fddc160c;
    address _mikolajAddress = 0x4b0897b0513fdc7c541b6d9d7e929c4e5364d2db;
    
    function InvestorDividendContract(){
        _shares[_simonAddress] = 34;
        _shares[_pawelAddress] = 33;
        _shares[_mikolajAddress] = 33;
    }
    
    function Donate() payable{
        // this function doesnt need an implementation
        // payable keyword guarantee us, that this function will accept payment
    }

    function Withdraw() returns (string){
        _simonAddress.transfer(CalculateDividend(_simonAddress));
        _pawelAddress.transfer(CalculateDividend(_pawelAddress));
        _mikolajAddress.transfer(CalculateDividend(_mikolajAddress));
        
        return "OK";
    }
    
    function CalculateDividend(address beneficientAddress) returns (uint){
        return  this.balance *  (_shares[beneficientAddress] / 100);
    }
    
    function GetBalance() constant returns (uint){
        return this.balance;
    }
}
