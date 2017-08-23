pragma solidity^0.4.0;

contract InvestorDividendContract {
    mapping(address => uint) _shares;
    mapping(address => bool) _permissions;
    
    address _simonAddress = 0xca35b7d915458ef540ade6068dfe2f44e8fa733c;
    address _pawelAddress = 0x14723a09acff6d2a60dcdf7aa4aff308fddc160c;
    address _mikolajAddress = 0x4b0897b0513fdc7c541b6d9d7e929c4e5364d2db;
    
    function InvestorDividendContract(){
        _shares[_simonAddress] = 34;
        _shares[_pawelAddress] = 33;
        _shares[_mikolajAddress] = 33;
    }
    
    modifier OnlyForPartner() {
        if(_shares[msg.sender] == 0){
            throw;
        }
        _;
    }
    
    function Donate() payable{
        // this function doesnt need an implementation
        // payable keyword guarantee us, that this function will accept payment
    }

    function IsWithdrawPermitted() private returns (bool){
        return _permissions[_simonAddress] 
                && _permissions[_pawelAddress] 
                && _permissions[_mikolajAddress];
    }
    
    function ResetPermissions() private {
        _permissions[_simonAddress] = false;
        _permissions[_pawelAddress] = false;
        _permissions[_mikolajAddress] = false;
    }
    
    function Withdraw() returns (string){
        
        _permissions[msg.sender] = true;
        
        if(!IsWithdrawPermitted()){
            return("All partners should ask for withdraw");
        }
        
        _simonAddress.transfer(CalculateDividend(_simonAddress));
        _pawelAddress.transfer(CalculateDividend(_pawelAddress));
        _mikolajAddress.transfer(CalculateDividend(_mikolajAddress));
        
        ResetPermissions();
        
        return "OK";
    }
    
    function CalculateDividend(address beneficientAddress) returns (uint){
        return  this.balance *  (_shares[beneficientAddress] / 100);
    }
    
    function GetBalance() OnlyForPartner constant returns (uint){
        return this.balance;
    }
}
