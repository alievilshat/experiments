<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts" xmlns:msxsl="urn:schemas-microsoft-com:xslt">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:table('str_supplier')">
          <address>
            <addressid m:left="-106" m:top="161">pk:address(supplier-<xsl:value-of select="id" />)</addressid>
            <streetaddress m:left="-106" m:top="193">
              <xsl:value-of select="address" />
            </streetaddress>
            <housenumber m:left="19" m:top="210">
              <xsl:value-of select="housenumber" />
            </housenumber>
            <housenumberadd m:left="-106" m:top="226">
              <xsl:value-of select="housenumberadd" />
            </housenumberadd>
            <recipient m:left="18" m:top="177">
              <xsl:value-of select="contactperson" />
            </recipient>
            <zipcode m:left="18" m:top="243">
              <xsl:value-of select="zipcode" />
            </zipcode>
            <city m:left="-106" m:top="260">
              <xsl:value-of select="city" />
            </city>
            <state m:left="20" m:top="275">
              <xsl:value-of select="state" />
            </state>
            <countryid m:left="-105" m:top="293">
              <xsl:value-of select="user:getcountry(countryid)" />
            </countryid>
          </address>
          <company>
            <companyid m:left="24" m:top="325">pk:company(supplier-<xsl:value-of select="id" />)</companyid>
            <phone m:left="26" m:top="355">
              <xsl:value-of select="phone" />
            </phone>
            <fax m:left="-114" m:top="371">
              <xsl:value-of select="fax" />
            </fax>
            <email m:left="25" m:top="389">
              <xsl:value-of select="email" />
            </email>
            <taxnumber m:left="-112" m:top="406">
              <xsl:value-of select="taxnumber" />
            </taxnumber>
            <bankaccount m:left="-81" m:top="573">
              <xsl:value-of select="bankaccountnumber" />
            </bankaccount>
            <iban m:left="44" m:top="591">
              <xsl:value-of select="iban" />
            </iban>
            <companyname m:left="-85" m:top="539">
              <xsl:value-of select="name" />
            </companyname>
            <companytypeid m:left="41" m:top="558">3</companytypeid>
            <inactive m:left="39" m:top="522">
              <xsl:value-of select="inactived" />
            </inactive>
            <supplierminimumorderamount m:left="-77" m:top="648">
              <xsl:value-of select="minorderamount" />
            </supplierminimumorderamount>
            <supplierdeliverytimeinstock m:left="-80" m:top="611">
              <xsl:value-of select="deliverytimeinstock" />
            </supplierdeliverytimeinstock>
            <supplierdeliverytimenotinstock m:left="48" m:top="629">
              <xsl:value-of select="deliverytimenotinstock" />
            </supplierdeliverytimenotinstock>
            <addressid m:left="-87" m:top="504">fk:address(supplier-<xsl:value-of select="id" />)</addressid>
            <companycreditline m:left="52" m:top="730">
              <xsl:value-of select="creditlimit" />
            </companycreditline>
          </company>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
  <msxsl:script implements-prefix="user" language="CSharp"><![CDATA[
    public string getcountry(string id)
    {
        if (id == "0") return "1";
        return id;
    }
    ]]></msxsl:script>
</xsl:stylesheet>