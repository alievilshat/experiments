<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select * from str_productsubsidiary where productid in (select id from str_product where not deleted and producttype = 0 and categoryid != 0 and (parentid is null or parentid in (select id from str_product where not deleted and producttype = 0 and categoryid != 0)))')">
          <productbusiness>
            <productbusinessid m:left="-110" m:top="160" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="id" />
            </productbusinessid>
            <productid m:left="62" m:top="176" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="productid" />
            </productid>
            <salesprice m:left="-112" m:top="192" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="salesprice" />
            </salesprice>
            <businessid m:left="-94" m:top="472" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="subsidiaryid" />
            </businessid>
            <buyprice m:left="60" m:top="210" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="buyprice" />
            </buyprice>
            <obsolete m:left="-112" m:top="244" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="obsolete" />
            </obsolete>
            <specialoffer m:left="-106" m:top="286" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="specialoffer" />
            </specialoffer>
            <onechoice m:left="62" m:top="264" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="onechoice" />
            </onechoice>
            <recommended m:left="64" m:top="308" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="recommended" />
            </recommended>
            <batchnumber m:left="-108" m:top="326" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="batchnumber" />
            </batchnumber>
            <expiredate m:left="64" m:top="350" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="expiredate" />
            </expiredate>
            <sourcetype m:left="-110" m:top="368" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="sourcetype" />
            </sourcetype>
            <sourceid m:left="64" m:top="386" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="sourceid" />
            </sourceid>
            <autoobsolete m:left="-108" m:top="400" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="autoobsolete" />
            </autoobsolete>
            <creationdate m:left="62" m:top="424" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="creationdate" />
            </creationdate>
          </productbusiness>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>