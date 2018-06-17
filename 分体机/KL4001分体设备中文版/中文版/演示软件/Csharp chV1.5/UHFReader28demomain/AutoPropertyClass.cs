using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Diagnostics;

namespace UHFReader28demomain
{
    /// <summary>
    /// 自动界面赋值取值对象；
    /// </summary>
    class AutoPropertyClass
    {
        private IntPtr _devHandle;

        private PropertyParaMapClass _propertyParaMap;

        /// <summary>
        /// 构造函数；
        /// </summary>
        /// <param name="devHandle">设备句柄；</param>
        public AutoPropertyClass(IntPtr devHandle)
        {
            this._devHandle = devHandle;

            this._propertyParaMap = new PropertyParaMapClass();
        }

        /// <summary>
        /// 添加控件与PARA_TYPES间的映射关系；
        /// </summary>
        /// <param name="propertyName">控件标识，使用控件的AccessibleName属性作为标识；</param>
        /// <param name="paraType">DevControl.PARA_TYPES</param>
        /// <returns>是否添加成功</returns>
        public bool AddPropertyParaMap(string propertyName, DevControl.PARA_TYPES paraType)
        {
            //增加控件与设备参数间的映射关系；
            return this._propertyParaMap.AddPropertyParaMap(propertyName, paraType);
        }

        /// <summary>
        /// 从容器控件中读取已加入映射表的子控件的输入值；
        /// </summary>
        /// <param name="ownerPage">容器控件；</param>
        /// <param name="channelNum">参数所属通道号；</param>
        /// <param name="paraList">IntPtr(paralist)；</param>
        /// <returns>DevControl.tagErrorCode</returns>
        private DevControl.tagErrorCode GetParameterFromProperty(Control ownerPage, int channelNum, IntPtr paraList)
        {
            IntPtr setParaList = paraList;
            DevControl.tagErrorCode eCode = DevControl.tagErrorCode.DM_ERR_OK;

            Debug.Assert(ownerPage != null);
            Debug.Assert(paraList != IntPtr.Zero);

            //遍历容器控件，通过控件名与设备参数间的映射关系，从配置界面的控件中读取配置设备的参数；
            foreach (Control control in ownerPage.Controls)
            {
                //控件不使能则不需要填充数值（包括其下所有子控件）
                if ((control.Enabled == false))
                {
                    continue;
                }

                //控件为非叶节点控件，则使用递归继续向下遍历；
                if (control.Controls.Count != 0)
                {
                    eCode = GetParameterFromProperty(control, channelNum, paraList);
                    if (eCode != DevControl.tagErrorCode.DM_ERR_OK)
                    {
                        break;
                    }
                }

                //使用control.AccessibleName来标识参与映射的控件；
                //可依据需求修改为其他标识；
                if (control.AccessibleName != null)
                {
                    bool isFind;

                    //查找控件映射的PARA_TYPES
                    DevControl.PARA_TYPES paraType = DevControl.PARA_TYPES.END_OF_PARA_TYPES;
                    isFind = this._propertyParaMap.FindParaByProperty(control.AccessibleName, ref paraType);
                    if (isFind == true)
                    {
                        //依据控件类型，从控件中读取输入参数值，按字符串格式读取；
                        StringBuilder bufferString = new StringBuilder();

                        Type controlType = control.GetType();
                        if (controlType == typeof(System.Windows.Forms.TextBox))
                        {
                            if (((System.Windows.Forms.TextBox)control).ReadOnly == true)
                            {
                                continue;
                            }

                            bufferString.Append(((System.Windows.Forms.TextBox)control).Text.Trim());
                        }
                        else if (controlType == typeof(System.Windows.Forms.CheckBox))
                        {
                            if (((System.Windows.Forms.CheckBox)control).Checked)
                            {
                                bufferString.Append('1');
                            }
                            else
                            {
                                bufferString.Append('0');
                            }
                        }
                        else if (controlType == typeof(System.Windows.Forms.ComboBox))
                        {
                            bufferString.Append(((System.Windows.Forms.ComboBox)control).SelectedIndex.ToString());
                        }
                        else if (controlType == typeof(System.Windows.Forms.NumericUpDown))
                        {
                            bufferString.Append(((System.Windows.Forms.NumericUpDown)control).Value.ToString());
                        }
                        else
                        {
                            Debug.Fail("must support new controlType!");
                            continue;
                        }

                        //将读取的字符串格式输入参数值转换为操作接口输入类型，然后检查输入参数的合法性，合法则加入参数列表；
                        byte[] valueBuf = new byte[100];
                        int valueLen = valueBuf.Length;
                        eCode = DevControl.DM_String2Value(paraType, bufferString, bufferString.Length, valueBuf, ref valueLen);
                        if (eCode == DevControl.tagErrorCode.DM_ERR_OK)
                        {
                            eCode = DevControl.DM_CheckPara(this._devHandle, channelNum, paraType, valueLen, valueBuf);
                            if (eCode == DevControl.tagErrorCode.DM_ERR_OK)
                            {
                                DevControl.paralist_addnode(setParaList, channelNum, paraType, valueLen, valueBuf);
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return eCode;
        }

        /// <summary>
        /// 将paralist里的数值填充入容器控件中的对应映射控件；
        /// </summary>
        /// <param name="paralist">IntPtr(paralist)；</param>
        /// <param name="ownerPage">容器控件；</param>
        /// <param name="channelNum">参数所属通道号；</param>
        /// <returns>是否成功</returns>
        private bool SetParameterToProperty(IntPtr paralist, Control ownerPage, int channelNum)
        {
            //通过控件名与设备参数间的映射关系，将从设备获取的参数值填充至对应的配置界面控件；
            if (paralist == IntPtr.Zero)
            {
                return false;
            }

            //遍历容器控件，通过控件名与设备参数间的映射关系，从参数列表中读取设备参数填充入对应控件；
            foreach (Control control in ownerPage.Controls)
            {
                //控件为非叶节点控件，则使用递归继续向下遍历；
                if (control.Controls.Count != 0)
                {
                    SetParameterToProperty(paralist, control, channelNum);
                }

                //使用control.AccessibleName来标识参与映射的控件；
                //可依据需求修改为其他标识；
                if (control.AccessibleName != null)
                {
                    bool isFind;

                    //查找控件映射的PARA_TYPES
                    DevControl.PARA_TYPES paraType = DevControl.PARA_TYPES.END_OF_PARA_TYPES;
                    isFind = this._propertyParaMap.FindParaByProperty(control.AccessibleName, ref paraType);
                    if (isFind == true)
                    {
                        byte[] valueBuf = new byte[100];
                        int valueLen = valueBuf.Length;

                        //使用PARA_TYPES从参数列表中读取设备参数填充入对应控件；
                        DevControl.tagErrorCode eCode = DevControl.paralist_getnode(paralist, channelNum, paraType, ref valueLen, valueBuf);
                        if (eCode == DevControl.tagErrorCode.DM_ERR_OK)
                        {
                            //当前设备不支持此参数；
                            if (valueLen == 0)
                            {
                                control.Enabled = false;
                                continue;
                            }

                            //将获取到的参数统一转换为字符串再按控件类型分别填充；
                            StringBuilder bufferString = new StringBuilder(100);
                            int stringLen = bufferString.Capacity;
                            eCode = DevControl.DM_Value2String(paraType, valueBuf, valueLen, bufferString, ref stringLen);
                            Debug.Assert(eCode == DevControl.tagErrorCode.DM_ERR_OK);

                            Type controlType = control.GetType();
                            if (controlType == typeof(System.Windows.Forms.TextBox))
                            {
                                ((System.Windows.Forms.TextBox)control).Text = bufferString.ToString();
                            }
                            else if (controlType == typeof(System.Windows.Forms.CheckBox))
                            {
                                if (bufferString[0] == '1')
                                {
                                    ((System.Windows.Forms.CheckBox)control).Checked = true;
                                }
                                else if (bufferString[0] == '0')
                                {
                                    ((System.Windows.Forms.CheckBox)control).Checked = false;
                                }
                                else
                                {
                                    Debug.Fail("out of range!");
                                }
                            }
                            else if (controlType == typeof(System.Windows.Forms.ComboBox))
                            {
                                if (valueBuf[0] < ((System.Windows.Forms.ComboBox)control).Items.Count)
                                {
                                    ((System.Windows.Forms.ComboBox)control).SelectedIndex = int.Parse(bufferString.ToString());
                                }
                                else
                                {
                                    Debug.Fail("out of range!");
                                }
                            }
                            else if (controlType == typeof(System.Windows.Forms.NumericUpDown))
                            {
                                ((System.Windows.Forms.NumericUpDown)(control)).Value = decimal.Parse(bufferString.ToString());
                            }
                            else
                            {
                                Debug.Fail("must support new controlType!");
                                continue;
                            }
                        }
                    }

                }
            }

            return true;
        }

        /// <summary>
        /// 依据容器控件中的对应映射控件，向远程设备请求对应的已配置参数；
        /// </summary>
        /// <param name="ownerPage">容器控件；</param>
        /// <param name="channelNum">参数所属通道号；</param>
        /// <param name="paraList">请求参数列表；</param>
        public void GetQueryParaList(Control ownerPage, int channelNum,IntPtr paraList)
        {
            //遍历容器控件，通过控件名与设备参数间的映射关系，生成向远程设备请求配置参数值的参数列表；
            foreach (Control control in ownerPage.Controls)
            {
                //控件不使能则不需要请求参数（包括其下所有子控件）
                if ((control.Enabled == false))
                {
                    continue;
                }

                //控件为非叶节点控件，则使用递归继续向下遍历；
                if (control.Controls.Count != 0)
                {
                    GetQueryParaList(control, channelNum, paraList);
                }

                //使用control.AccessibleName来标识参与映射的控件；
                //可依据需求修改为其他标识；
                if (control.AccessibleName != null)
                {
                    bool isFind;

                    DevControl.PARA_TYPES paraType = DevControl.PARA_TYPES.END_OF_PARA_TYPES;
                    isFind = this._propertyParaMap.FindParaByProperty(control.AccessibleName, ref paraType);
                    if (isFind == true)
                    {
                        DevControl.paralist_addnode(paraList, channelNum, paraType);
                    }
                }
            }
        }

        /// <summary>
        /// 依据容器控件中的对应映射控件，从设备获取参数并填充对应映射控件；
        /// </summary>
        /// <param name="ownerPage">容器控件；</param>
        /// <param name="channelNum">参数所属通道号；</param>
        /// <returns>tagErrorCode</returns>
        public DevControl.tagErrorCode GetParameter(Control ownerPage, int channelNum)
        {
            IntPtr getParaList;
            //从远程设备获取其当前配置参数；
            getParaList = DevControl.paralistCreate(this._devHandle);
            if (getParaList == IntPtr.Zero)
            {
                return DevControl.tagErrorCode.DM_ERR_MEM;
            }

            GetQueryParaList(ownerPage, channelNum, getParaList);

            DevControl.tagErrorCode errCode = DevControl.DM_GetPara(this._devHandle, getParaList, 1000);
            if (errCode == DevControl.tagErrorCode.DM_ERR_OK)
            {
                SetParameterToProperty(getParaList, ownerPage, channelNum);
            }

            DevControl.paralistDestroy(getParaList);

            return errCode;
        }

        /// <summary>
        /// 依据容器控件中的对应映射控件，将本地配置参数配置到远程设备；
        /// </summary>
        /// <param name="ownerPage">容器控件；</param>
        /// <param name="channelNum">参数所属通道号；</param>
        /// <returns>tagErrorCode</returns>
        public DevControl.tagErrorCode SetParameter(Control ownerPage, int channelNum)
        {
            IntPtr setParaList;
            DevControl.tagErrorCode eCode;

            //将配置参数设置到对应远程设备；

            setParaList = DevControl.paralistCreate(this._devHandle);
            if (setParaList == IntPtr.Zero)
            {
                return DevControl.tagErrorCode.DM_ERR_MEM;
            }

            eCode = GetParameterFromProperty(ownerPage, channelNum, setParaList);
            if (eCode == DevControl.tagErrorCode.DM_ERR_OK)
            {
                Debug.Assert(setParaList != IntPtr.Zero);
                eCode = DevControl.DM_SetPara(this._devHandle, setParaList, 1000);
                DevControl.paralistDestroy(setParaList);
            }
            return eCode;
        }
    }

    /// <summary>
    /// 控件标识与PARA_TYPES间映射关系管理对象；
    /// 暂时只支持一对一映射；
    /// </summary>
    class PropertyParaMapClass
    {
        private string[] propertyParaMap;

        public PropertyParaMapClass()
        {
            int maxPara = (int)DevControl.PARA_TYPES.END_OF_PARA_TYPES;
            this.propertyParaMap = new string[maxPara];
        }

        /// <summary>
        /// 添加控件与PARA_TYPES间的映射关系；
        /// </summary>
        /// <param name="propertyName">控件标识；</param>
        /// <param name="paraType">DevControl.PARA_TYPES</param>
        /// <returns>是否添加成功</returns>
        public bool AddPropertyParaMap(string propertyName, DevControl.PARA_TYPES paraType)
        {
            int mapIndex = (int)paraType;

            Debug.Assert(mapIndex < this.propertyParaMap.Length) ;
            Debug.Assert(this.propertyParaMap[mapIndex] == null) ;

            this.propertyParaMap[mapIndex] = propertyName;

            return true;
        }

        /// <summary>
        /// 使用控件标识查找其所对应的PARA_TYPES；
        /// </summary>
        /// <param name="propertyName">控件标识；</param>
        /// <param name="paraType">DevControl.PARA_TYPES</param>
        /// <returns>是否添加成功</returns>
        public bool FindParaByProperty(string propertyName, ref DevControl.PARA_TYPES paraType)
        {
            int index = 0;
            foreach (string name in this.propertyParaMap)
            {
                if (propertyName.Equals(name))
                {
                    paraType = (DevControl.PARA_TYPES)index;
                    return true;
                }

                index++;
            }

            return false;
        }

        /// <summary>
        /// 使用PARA_TYPES查找其所对应的控件标识；
        /// </summary>
        /// <param name="paraType">DevControl.PARA_TYPES</param>
        /// <returns>对应的控件标识</returns>
        public string FindPropertyByPara(DevControl.PARA_TYPES paraType)
        {
            return (string)this.propertyParaMap[(int)paraType];
        }
    }
}
