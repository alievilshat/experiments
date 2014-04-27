<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select * from str_client where clienttypeid  =0 and not isdeleted')">
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
          <person>
            <personid m:left="2" m:top="319">pk:person(client-<xsl:value-of select="id" />)</personid>
            <xsl:if test="companyid != 'NULL'">
              <companyid m:left="-121" m:top="337">fk:company(company-<xsl:value-of select="id" />)</companyid>
            </xsl:if>
            <firstname m:left="-117" m:top="383">
              <xsl:value-of select="firstname" />
            </firstname>
            <title m:left="5" m:top="368">
              <xsl:value-of select="title" />
            </title>
            <infix m:left="7" m:top="399">
              <xsl:value-of select="infix" />
            </infix>
            <lastname m:left="-115" m:top="414">
              <xsl:value-of select="lastname" />
            </lastname>
            <gender m:left="5" m:top="430">
              <xsl:value-of select="sex" />
            </gender>
            <phoneprivate m:left="-118" m:top="445">
              <xsl:value-of select="phoneprivate" />
            </phoneprivate>
            <phonebusiness m:left="5" m:top="462">
              <xsl:value-of select="phonebusiness" />
            </phonebusiness>
            <fax m:left="-119" m:top="478">
              <xsl:value-of select="fax" />
            </fax>
            <phonemobile m:left="6" m:top="495">
              <xsl:value-of select="phonemobile" />
            </phonemobile>
            <email m:left="-119" m:top="511">
              <xsl:value-of select="email" />
            </email>
            <allownewsletter m:left="4" m:top="528">
              <xsl:value-of select="allownewsletter" />
            </allownewsletter>
            <inactive m:left="15" m:top="675">
              <xsl:value-of select="inactived" />
            </inactive>
            <password m:left="-110" m:top="626">
              <xsl:value-of select="password" />
            </password>
            <businessid m:left="11" m:top="644">
              <xsl:value-of select="businessid" />
            </businessid>
          </person>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>