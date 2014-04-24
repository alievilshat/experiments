<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select * from str_client where clienttypeid = 1 and not isdeleted')">
          <address>
            <addressid m:left="-2" m:top="164">pk:address(company-<xsl:value-of select="id" />)</addressid>
            <streetaddress m:left="0" m:top="196">
              <xsl:value-of select="billingaddress" />
            </streetaddress>
            <housenumber m:left="-124" m:top="211">
              <xsl:value-of select="billinghousenumber" />
            </housenumber>
            <housenumberadd m:left="0" m:top="227">
              <xsl:value-of select="billinghousenumberadd" />
            </housenumberadd>
            <zipcode m:left="-124" m:top="241">
              <xsl:value-of select="billingzipcode" />
            </zipcode>
            <city m:left="0" m:top="256">
              <xsl:value-of select="billingcity" />
            </city>
            <countryid m:left="0" m:top="288">
              <xsl:value-of select="billingcountryid" />
            </countryid>
            <state m:left="-128" m:top="273">
              <xsl:value-of select="billingstate" />
            </state>
            <recipient m:left="-125" m:top="180">
              <xsl:value-of select="deliveryrecipient" />
            </recipient>
          </address>
          <company>
            <companyid m:left="-11" m:top="324">pk:company(company-<xsl:value-of select="id" />)</companyid>
            <companyname m:left="-144" m:top="541">
              <xsl:value-of select="firstname" />
            </companyname>
            <phone m:left="-10" m:top="356">
              <xsl:value-of select="phonebusiness" />
            </phone>
            <fax m:left="-132" m:top="371">
              <xsl:value-of select="fax" />
            </fax>
            <email m:left="-9" m:top="387">
              <xsl:value-of select="email" />
            </email>
            <taxnumber m:left="-131" m:top="404">
              <xsl:value-of select="taxnumber" />
            </taxnumber>
            <chamberofcommerce m:left="-10" m:top="419">
              <xsl:value-of select="commersechamber" />
            </chamberofcommerce>
            <companytypeid m:left="-22" m:top="558">2</companytypeid>
            <inactive m:left="-21" m:top="526">
              <xsl:value-of select="inactived" />
            </inactive>
            <url m:left="-9" m:top="451">
              <xsl:value-of select="url" />
            </url>
            <businessid m:left="-133" m:top="339">
              <xsl:value-of select="businessid" />
            </businessid>
            <addressid m:left="-147" m:top="508">fk:address(company-<xsl:value-of select="id" />)</addressid>
          </company>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>