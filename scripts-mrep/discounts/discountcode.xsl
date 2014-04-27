<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select * from str_discountcode o inner join str_discountgroup g on g.groupid = o.groupid where g.giveawayid is null or g.giveawayid in (select id from str_product where not deleted and true and (parentid is null or parentid in (select id from str_product where not deleted and true))) ')">
          <discountcode>
            <discountcodeid m:left="-125" m:top="161">
              <xsl:value-of select="codeid" />
            </discountcodeid>
            <code m:left="5" m:top="177">
              <xsl:value-of select="code" />
            </code>
            <groupid m:left="-123" m:top="193">
              <xsl:value-of select="groupid" />
            </groupid>
            <datecreated m:left="7" m:top="211">
              <xsl:value-of select="datecreated" />
            </datecreated>
          </discountcode>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>