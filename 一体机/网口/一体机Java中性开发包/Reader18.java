/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package UHF;

/**
 *
 * @author zoudeyong
 */
public class Reader18 {

    /**
     * @param args the command line arguments
     */
    public native int[] OpenComPort(int[]arr);
    public native int[] AutoOpenComPort(int[]arr);
    public native int CloseComPort();
    public native int CloseSpecComPort(int Frmhandle);
    public native int[] GetReaderInformation(int[]arr);
    public native int SetWGParameter(int[]arr);
    public native int[] ReadActiveModeData(int[]arr);
    public native int SetWorkMode(int[]arr);
    public native int[] GetWorkModeParameter(int[]arr);
    public native int BuzzerAndLEDControl(int[] arr);
    public native int WriteComAdr(int[] arr);
    public native int SetPowerDbm(int[] arr);
    public native int Writedfre(int[] arr);
    public native int Writebaud(int[] arr);
    public native int WriteScanTime(int[] arr);
    public native int SetAccuracy(int[] arr);
    //EPC  G2
    public native int[] Inventory_G2(int[]arr);
    public native int[] ReadCard_G2(int[]arr);
    public native int[] WriteCard_G2(int[]arr);
    public native int[] EraseCard_G2(int[]arr);
    public native int[] SetCardProtect_G2(int[]arr);
    public native int[] DestroyCard_G2(int[]arr);
    public native int[] WriteEPC_G2(int[]arr);
    public native int[] SetReadProtect_G2(int[]arr);
    public native int[] SetMultiReadProtect_G2(int[]arr);
    public native int[] RemoveReadProtect_G2(int[]arr);
    public native int[] CheckReadProtected_G2(int[]arr);
    public native int[] SetEASAlarm_G2(int[]arr);
    public native int[] CheckEASAlarm_G2(int[]arr);
    public native int[] LockUserBlock_G2(int[]arr);
    //18000_6B
    public native int[] Inventory_6B(int[]arr);
    public native int[] inventory2_6B(int[]arr);
    public native int[] ReadCard_6B(int[]arr);
    public native int[] WriteCard_6B(int[]arr);
    public native int[] LockByte_6B(int[]arr);
    public native int[] CheckLock_6B(int[]arr);
    public static void main(String[] args) {
        // TODO code application logic here
    }

}
